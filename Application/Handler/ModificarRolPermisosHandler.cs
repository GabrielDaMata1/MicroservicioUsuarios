using MassTransit;
using MediatR;
using MicroserviciosUsuarios.Domain.Entities;
using MicroserviciosUsuarios.Domain.Events;
using MicroserviciosUsuarios.Infrastructure.Repositories.PostgreSQL;
using MicroservicioUsuarios.Application.Command;
using MicroservicioUsuarios.Application.Services;
using MicroservicioUsuarios.Domain.Events;

namespace MicroservicioUsuarios.Application.Handler
{
    public class ModificarPermisosRolHandler : IRequestHandler<ModificarPermisosRolCommand, bool>
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IPublishEndpoint _publishEndpoint;

        public ModificarPermisosRolHandler(IUsuarioService usuarioService, IPublishEndpoint publishEndpoint)
        {
            _usuarioService = usuarioService;
            _publishEndpoint = publishEndpoint;

        }

        public async Task<bool> Handle(ModificarPermisosRolCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var idRol = await _usuarioService.ObtenerIdRolPorNombreMongoAsync(request.PermisosRolDto.NombreRol);
                if (idRol == null)
                {
                    throw new Exception($"El rol '{request.PermisosRolDto.NombreRol}' no fue encontrado.");
                }
                var idsPermisos = await _usuarioService.ObtenerIdsPermisosMongoAsync(request.PermisosRolDto.NombresPermisos);
                if (idsPermisos == null || !idsPermisos.Any())
                {
                    throw new Exception("No se pudieron obtener los IDs de todos los permisos solicitados.");
                }
                var resul = await _usuarioService.ModificarPermisosRolPostgresAsync(idRol, idsPermisos);

                if (resul)
                {
                    await _publishEndpoint.Publish(new RolPermisosModificadoEvent(idRol, idsPermisos));
                    return true; 
                }
                else
                {
                    return false; 
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error en la modificación de permisos para el rol: {ex.Message}");
                return false;
            }
        }


        /*   public async Task<bool> Handle(ModificarPermisosRolCommand request, CancellationToken cancellationToken)
           {
               var idRol = await _usuarioService.ObtenerIdRolPorNombreMongoAsync(request.PermisosRolDto.NombreRol);
               if (idRol == null) return false;

               var idsPermisos = await _usuarioService.ObtenerIdsPermisosMongoAsync(request.PermisosRolDto.NombresPermisos);
               if (idsPermisos == null || idsPermisos.Count == 0) 
                   return false;

               var resul= await _usuarioService.ModificarPermisosRolPostgresAsync(idRol, idsPermisos);
               if (resul)
               {
                   await _publishEndpoint.Publish(new RolPermisosModificadoEvent(idRol,idsPermisos));
                   return resul;
               } else
               {
                   return false;
               }
           }*/
    }

}
