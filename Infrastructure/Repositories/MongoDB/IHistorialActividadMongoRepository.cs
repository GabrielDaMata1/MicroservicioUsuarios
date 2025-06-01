using System.Net;
using MicroservicioUsuarios.Infrastructure.Models;

namespace MicroserviciosUsuarios.Infrastructure.Repositories.MongoDB
{
    public interface IHistorialActividadMongoRepository
    {
        Task<HttpStatusCode> RegistrarActividadAsync(HistorialActividadMongo historial);
        Task<List<HistorialActividadMongo>> ObtenerHistorialPorCorreoAsync(Guid IdUsuario);

    }
}