using MediatR;
using MassTransit;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MicroservicioUsuarios.Application.Command;
using MicroserviciosUsuarios.Application.Mappers;
using MicroserviciosUsuarios.Infrastructure.Repositories.PostgreSQL;
using MicroserviciosUsuarios.Infrastructure.Repositories.MongoDB;
using MicroserviciosUsuarios.Infrastructure.Repositories.Keycloak;
using MicroserviciosUsuarios.Infrastructure.Events;
using MicroserviciosUsuarios.Domain.Events;
using MicroserviciosUsuarios.Domain.Entities;
using MicroservicioUsuarios.Domain.Factory;
using MicroservicioUsuarios.Application.Exceptions;
using MicroservicioUsuarios.Infrastructure.Models;
using MicroservicioUsuarios.Application.Services;

namespace MicroserviciosUsuarios.Application.Handler
{
    public class ActualizarPerfilUsuarioHandler : IRequestHandler<ActualizarPerfilUsuarioCommand, bool>
{
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IUsuarioService _usuarioService;
    private readonly IHistorialActividadServices _historialActividadServices;

        public ActualizarPerfilUsuarioHandler(IUsuarioService usuarioService, IPublishEndpoint publishEndpoint, IHistorialActividadServices historialActividadServices)
        {
        _publishEndpoint = publishEndpoint;
        _usuarioService = usuarioService;
        _historialActividadServices = historialActividadServices;
        }

        public async Task<bool> Handle(ActualizarPerfilUsuarioCommand request, CancellationToken cancellationToken)
        {
            var idUsuario = await _usuarioService.ObtenerGuidPorCorreoMongoAsync(request.correo);   
           if (idUsuario == null || idUsuario == Guid.Empty)
                throw new UsuarioNoEncontradoException();

            var usuarioEntity = UsuarioFactory.CrearUsuarioConId(idUsuario,request.actualizarPerfilDTO.Nombre,request.actualizarPerfilDTO.Apellido, request.actualizarPerfilDTO.Correo, request.actualizarPerfilDTO.Telefono, request.actualizarPerfilDTO.Direccion);
            var usuarioPostgres = usuarioEntity.ToPostgres();
            var IdRol = await _usuarioService.ObtenerIdRolPorCorreoMongoAsync(request.correo);
            usuarioPostgres.RolId=IdRol;
            var resul=await _usuarioService.ActualizarUsuarioPostgresAsync(request.correo, usuarioPostgres);
            if (resul == HttpStatusCode.OK)
            {
                var IdActividad = Guid.NewGuid();
                var TipoActividad = "Actualización de perfil";
                var Fecha = DateTime.UtcNow;
                var actividad = new HistorialActividad
                {
                Id = IdActividad,
                TipoAccion = TipoActividad,
                FechaHora = Fecha,
                UsuarioId = idUsuario
                };
                await _historialActividadServices.RegistrarActividadPostgresAsync(actividad);
                await _usuarioService.ActualizarUsuarioEnKeycloakAsync(request.actualizarPerfilDTO.userId,
                    request.actualizarPerfilDTO.Nombre, request.actualizarPerfilDTO.Apellido,
                    request.actualizarPerfilDTO.Correo);
                await _publishEndpoint.Publish(new UsuarioModificadoEvent(idUsuario, request.actualizarPerfilDTO.Nombre, request.actualizarPerfilDTO.Apellido, request.actualizarPerfilDTO.Correo, request.actualizarPerfilDTO.Telefono, request.actualizarPerfilDTO.Direccion,IdRol,request.correo));
                await _publishEndpoint.Publish(new ActividadRegistradaEvent(IdActividad, idUsuario, TipoActividad,Fecha));
                return true;
            } else
            {
                return false;
            }

        }       
    }
}