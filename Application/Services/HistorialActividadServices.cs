using System.Net;
using MicroserviciosUsuarios.Domain.Entities;
using MicroserviciosUsuarios.Infrastructure.Repositories.MongoDB;
using MicroserviciosUsuarios.Infrastructure.Repositories.PostgreSQL;
using MicroservicioUsuarios.Application.Exceptions;
using MicroservicioUsuarios.Infrastructure.Models;

namespace MicroservicioUsuarios.Application.Services
{
    public class HistorialActividadServices: IHistorialActividadServices
    {
        private readonly IHistorialActividadMongoRepository _historialMongoRepository;
        private readonly IHistorialActividadRepository _historialRepository;

        public HistorialActividadServices (IHistorialActividadMongoRepository historialMongoRepository, IHistorialActividadRepository historialRepository)
        {
            _historialMongoRepository = historialMongoRepository;
            _historialRepository = historialRepository;
        }

        public async Task<Guid> RegistrarActividadPostgresAsync(HistorialActividad historial)
        {
            if (historial == null)
                throw new HistorialVacioException();
            try
            {
                var resul = await _historialRepository.RegistrarActividadAsync(historial);
                return resul;
            }
            catch (Exception ex)
            {
                throw new HistorialActividadRepositoryException($"Error al intentar registrar la actividad en MongoDB: {ex.Message}", ex);
            }
        }

        public async Task<HttpStatusCode> RegistrarActividadMongoAsync(HistorialActividadMongo historial)
        {
            if (historial == null)
                throw new HistorialVacioException();
            try
            {
                var resul = await _historialMongoRepository.RegistrarActividadAsync(historial);
                return resul;
            }
            catch (Exception ex)
            {
                throw new HistorialActividadRepositoryException($"Error al intentar registrar la actividad en MongoDB: {ex.Message}", ex);
            }
        }
        public async Task<List<HistorialActividadMongo>> ObtenerHistorialPorCorreoMongoAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new GuidInvalidoException();

            try
            {
                var resul = await _historialMongoRepository.ObtenerHistorialPorCorreoAsync(id);
                return resul;
            }
            catch (Exception ex)
            {
                throw new HistorialActividadRepositoryException($"Error al intentar obtener la actividad en MongoDB: {ex.Message}", ex);
            }
        }
    }

}

