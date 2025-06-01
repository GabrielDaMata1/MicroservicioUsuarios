using System.Net;
using MicroservicioUsuarios.Infrastructure.Models;

namespace MicroservicioUsuarios.Application.Services
{
    public interface IHistorialActividadServices
    {
        Task<HttpStatusCode> RegistrarActividadMongoAsync(HistorialActividadMongo historial);
        Task<List<HistorialActividadMongo>> ObtenerHistorialPorCorreoMongoAsync(Guid IdUsuario);

        Task<Guid> RegistrarActividadPostgresAsync(HistorialActividad historial);
    }
}
