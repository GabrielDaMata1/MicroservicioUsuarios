using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using MicroserviciosUsuarios.Infrastructure.Persistance;
using MicroservicioUsuarios.Infrastructure.Models;
namespace MicroserviciosUsuarios.Infrastructure.Repositories.PostgreSQL
{
    public class HistorialActividadRepository : IHistorialActividadRepository
    {
        private readonly SubastaDbContext _dbContext;

        public HistorialActividadRepository(SubastaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> RegistrarActividadAsync(HistorialActividad historial)
        {
            await _dbContext.Historial_Actividad.AddAsync(historial);
            await _dbContext.SaveChangesAsync();
            return historial.Id;
        }

    }
}