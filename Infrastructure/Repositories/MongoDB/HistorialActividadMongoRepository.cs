using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using MicroservicioUsuarios.Infrastructure.Models;
namespace MicroserviciosUsuarios.Infrastructure.Repositories.MongoDB
{
    public class HistorialActividadMongoRepository : IHistorialActividadMongoRepository
{
    private readonly IMongoCollection<HistorialActividadMongo> _historialCollection;

    public HistorialActividadMongoRepository(IMongoClient mongoClient)
    {
            var database = mongoClient.GetDatabase("SubastasDB");
            _historialCollection = database.GetCollection<HistorialActividadMongo>("HistorialActividad");
    }

    public async Task<HttpStatusCode> RegistrarActividadAsync(HistorialActividadMongo historial)
    {
            await _historialCollection.InsertOneAsync(historial);
            return HttpStatusCode.OK;
    }

        public async Task<List<HistorialActividadMongo>> ObtenerHistorialPorCorreoAsync(Guid idUsuario)
        {
            var filtro = Builders<HistorialActividadMongo>.Filter.Eq(h => h.UsuarioId, idUsuario);
            return await _historialCollection.Find(filtro).SortByDescending(h => h.FechaHora).ToListAsync();
        }


    }
}
