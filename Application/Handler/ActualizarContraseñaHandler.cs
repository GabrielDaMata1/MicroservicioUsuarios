using MediatR;
using MassTransit;
using System;
using System.Threading;
using System.Threading.Tasks;
using MicroservicioUsuarios.Application.Command;
using MicroservicioUsuarios.Application.Exceptions;
using MicroserviciosUsuarios.Infrastructure.Repositories.MongoDB;
using MicroserviciosUsuarios.Infrastructure.Repositories.PostgreSQL;
using MicroservicioUsuarios.Infrastructure.Models;
using MicroserviciosUsuarios.Infrastructure.Events;
using MicroservicioUsuarios.Application.Services;
using System.Net;

namespace MicroserviciosUsuarios.Application.Handler
{

    public class ActualizarContraseñaHandler : IRequestHandler<ActualizarContraseñaCommand, bool>
{
    private readonly IUsuarioService _usuarioService;
    private readonly IHistorialActividadServices _historialActividadServices;
    private readonly IPublishEndpoint _publishEndpoint;



    public ActualizarContraseñaHandler(IHistorialActividadServices historialActividadServices, IUsuarioService usuarioService, IPublishEndpoint publishEndpoint)
    {
            _usuarioService = usuarioService;
            _historialActividadServices = historialActividadServices;
            _publishEndpoint = publishEndpoint;
     }

    public async Task<bool> Handle(ActualizarContraseñaCommand request, CancellationToken cancellationToken)
    {       
            
           var idUsuario = await _usuarioService.ObtenerGuidPorCorreoMongoAsync(request.Correo);   

           if (idUsuario == null || idUsuario == Guid.Empty)
                throw new UsuarioNoEncontradoException();

           var keycloakResult = await _usuarioService.CambiarContrasenaKeycloakAsync(request.contraseñaDTO.userId, request.contraseñaDTO.Contraseña);

           if (keycloakResult == HttpStatusCode.OK || keycloakResult == HttpStatusCode.NoContent)
           {
               var IdActividad = Guid.NewGuid();
               var TipoActividad = "Actualización de contraseña";
               var Fecha = DateTime.UtcNow;

               var actividad = new HistorialActividad
               {
                   Id = IdActividad,
                   TipoAccion = TipoActividad,
                   FechaHora = Fecha,
                   UsuarioId = idUsuario
               };

               await _historialActividadServices.RegistrarActividadPostgresAsync(actividad);
               await _publishEndpoint.Publish(new ActividadRegistradaEvent(IdActividad, idUsuario, TipoActividad, Fecha));

               return true;
           }
           else
           {
               return false;
           }
    }
  }
}