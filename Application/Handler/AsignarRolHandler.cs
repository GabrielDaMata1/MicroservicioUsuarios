using System.Net;
using MassTransit;
using MediatR;
using MicroserviciosUsuarios.Application.Mappers;
using MicroserviciosUsuarios.Domain.Entities;
using MicroserviciosUsuarios.Domain.Events;
using MicroservicioUsuarios.Application.Command;
using MicroservicioUsuarios.Application.Exceptions;
using MicroservicioUsuarios.Application.Services;
using MicroservicioUsuarios.Infrastructure.Models;

namespace MicroservicioUsuarios.Application.Handler
{
    public class AsignarRolHandler : IRequestHandler<AsignarRolCommand, bool>

    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IUsuarioService _usuarioService;
        private readonly IHistorialActividadServices _historialActividadServices;

        public AsignarRolHandler(IUsuarioService usuarioService, IPublishEndpoint publishEndpoint, IHistorialActividadServices historialActividadServices)
        {
            _publishEndpoint = publishEndpoint;
            _usuarioService = usuarioService;
            _historialActividadServices = historialActividadServices;
        }
        public async Task<bool> Handle(AsignarRolCommand request, CancellationToken cancellationToken)
        {
            var usuario = await _usuarioService.ObtenerUsuarioMongoPorCorreoAsync(request.CorreoUsuario);
            if (usuario == null)
                throw new UsuarioNoEncontradoException();

            usuario.RolId = await _usuarioService.ObtenerIdRolPorNombreMongoAsync(request.NombreRol.nombre_rol);
            var resul= await _usuarioService.ActualizarUsuarioPostgresAsync(request.CorreoUsuario, usuario.FromMongoToPostgres());
            if (resul == HttpStatusCode.OK)
            {

                await _usuarioService.AsignarRolUsuario(request.NombreRol.userId,
                    request.NombreRol.nombre_rol);
                await _publishEndpoint.Publish(new UsuarioModificadoEvent(usuario.Id, usuario.Nombre, usuario.Apellido, usuario.Correo, usuario.Telefono, usuario.Direccion, usuario.RolId, request.CorreoUsuario));
                return true;
            } else
            {
                return false;
            }
        }

    }
}
