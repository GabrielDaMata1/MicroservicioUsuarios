using MicroservicioUsuarios.Infrastructure.Models;

namespace MicroserviciosUsuarios.Infrastructure.Repositories.PostgreSQL
{
	public interface IHistorialActividadRepository
{
        Task<Guid> RegistrarActividadAsync(HistorialActividad historial);
}
}