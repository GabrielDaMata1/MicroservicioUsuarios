using MongoDB.Driver;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading.Tasks;

public class RolRepository : IRolRepository
{
    private readonly IMongoCollection<BsonDocument> _rolesPermisosCollection;
    private readonly IMongoCollection<RolMongo> _rolesCollection;

    public RolRepository(IMongoClient mongoClient)
    {
        var database = mongoClient.GetDatabase("SubastasDB");
        _rolesPermisosCollection = database.GetCollection<BsonDocument>("RolesPermisos");
        _rolesCollection = database.GetCollection<RolMongo>("Roles");
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
}