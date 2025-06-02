using MongoDB.Driver;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading.Tasks;
using MicroservicioUsuarios.Infrastructure.Models;
using MicroservicioUsuarios.Application.Exceptions;
using System.Net;
using MicroservicioUsuarios.Application.DTOs;
using MongoDB.Driver;
public class RolMongoRepository : IRolMongoRepository
{
    private readonly IMongoCollection<RolesPermisosMongo> _rolesPermisosCollection;
    private readonly IMongoCollection<RolMongo> _rolesCollection;
    private readonly IMongoCollection<PermisoMongo> _permisoCollection;

    public RolMongoRepository(IMongoClient mongoClient)
    {
        var database = mongoClient.GetDatabase("SubastasDB");
        _rolesPermisosCollection = database.GetCollection<RolesPermisosMongo>("RolesPermisos");
        _rolesCollection = database.GetCollection<RolMongo>("Roles");
        _permisoCollection = database.GetCollection<PermisoMongo>("Permisos");
    }

    public async Task<List<RolConPermisosDTO>> ObtenerRolesConPermisosAsync()
    {
        var pipeline = new[]
        {

            new BsonDocument("$lookup", new BsonDocument
            {
                { "from", "Roles" },
                { "localField", "_id.rolId" },
                { "foreignField", "_id" },
                { "as", "rolInfo" }
            }),

            new BsonDocument("$lookup", new BsonDocument
            {
                { "from", "Permisos" },
                { "localField", "_id.permisoId" },
                { "foreignField", "_id" },
                { "as", "permisoInfo" }
            }),
            new BsonDocument("$unwind", "$rolInfo"),
            new BsonDocument("$unwind", "$permisoInfo"),

            new BsonDocument("$project", new BsonDocument
            {
                { "_id", 0 },
                { "rolNombre", "$rolInfo.Nombre" },
                { "permisoNombre", "$permisoInfo.nombre" }
            }),

            new BsonDocument("$group", new BsonDocument
            {
                { "_id", "$rolNombre" },
                { "permisos", new BsonDocument("$push", "$permisoNombre") }
            })
        };

        return await _rolesPermisosCollection.Aggregate<RolConPermisosDTO>(pipeline).ToListAsync();
    }

    public async Task<List<int>> ObtenerIdsPermisosAsync(List<string> nombresPermisos)
    {
        if (nombresPermisos == null || !nombresPermisos.Any())
        {
            return new List<int>();
        }

        var filtro = Builders<PermisoMongo>.Filter.In(p => p.Nombre, nombresPermisos);

        var permisos = await _permisoCollection.Find(filtro).ToListAsync();
        return permisos.Select(p => p.Id).ToList();
    }

    public async Task<bool> ModificarPermisosRolAsync(int rolId, List<int> nuevosPermisos)
    {
        if (nuevosPermisos == null || !nuevosPermisos.Any())
        {
            return false;
        }

        var filtro = Builders<RolesPermisosMongo>.Filter.Eq(rp => rp.Id.RolId, rolId);

        var permisosActuales = await _rolesPermisosCollection.Find(filtro)
                                                              .Project(rp => rp.Id.PermisoId)
                                                              .ToListAsync();


        var permisosAAgregar = nuevosPermisos.Except(permisosActuales).ToList();

        if (!permisosAAgregar.Any())
        {
            var permisosDuplicados = nuevosPermisos.Intersect(permisosActuales).ToList();
            throw new PermisoYaRegistradoException(
                $"Todos los permisos ({string.Join(", ", permisosDuplicados)}) que intentas añadir ya están asignados al rol {rolId}.");
        }

        var documentosAInsertar = permisosAAgregar.Select(permisoId => new RolesPermisosMongo
        {
            Id = new RolPermisosIdMongo { RolId = rolId, PermisoId = permisoId }
        }).ToList();

        try
        {
            await _rolesPermisosCollection.InsertManyAsync(documentosAInsertar);
            return true;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error al añadir permisos a rol {rolId}: {ex.Message}");
            throw;
        }
    }





}