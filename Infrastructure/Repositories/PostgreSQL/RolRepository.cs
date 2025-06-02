using MicroserviciosUsuarios.Infrastructure.Persistance;
using MicroservicioUsuarios.Application.Exceptions;
using MicroservicioUsuarios.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroservicioUsuarios.Infrastructure.Repositories.PostgreSQL
{
    public class RolRepository: IRolRepository
    {
        private readonly SubastaDbContext _dbContext;

        public RolRepository(SubastaDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> ModificarPermisosRolAsync(int rolId, List<int> nuevosPermisos)
        {
            if (nuevosPermisos == null || !nuevosPermisos.Any())
            {
                throw new ArgumentException("La lista de nuevos permisos no puede ser nula o vacía.", nameof(nuevosPermisos));
            }

            var permisosActuales = await _dbContext.RolesPermisos
                .Where(rp => rp.RolId == rolId)
                .Select(rp => rp.PermisoId)
                .ToListAsync();

            var permisosAAgregar = nuevosPermisos.Except(permisosActuales).ToList();

            if (!permisosAAgregar.Any())
            {
                var permisosDuplicados = nuevosPermisos.Intersect(permisosActuales).ToList();

                throw new PermisoYaRegistradoException(
                    $"Los permisos ({string.Join(", ", permisosDuplicados)}) que intentas añadir ya están asignados al rol {rolId}.");
            }

            var nuevosRolPermisos = permisosAAgregar.Select(permisoId => new RolPermisos
            {
                RolId = rolId,
                PermisoId = permisoId
            }).ToList();

            _dbContext.RolesPermisos.AddRange(nuevosRolPermisos);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
