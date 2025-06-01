using MediatR;
using MassTransit;
using System;
using System.Threading;
using System.Threading.Tasks;
using MicroservicioUsuarios.Application.Command;
using MicroserviciosUsuarios.Application.Mappers;
using MicroserviciosUsuarios.Infrastructure.Repositories.PostgreSQL;
using MicroserviciosUsuarios.Infrastructure.Repositories.MongoDB;
using MicroserviciosUsuarios.Infrastructure.Repositories.Keycloak;
using MicroserviciosUsuarios.Domain.Events;
using MicroserviciosUsuarios.Domain.Entities;
using MicroservicioUsuarios.Domain.Factory;
using MicroservicioUsuarios.Application.Exceptions;
using MicroservicioUsuarios.Application.Services;

namespace MicroserviciosUsuarios.Application.Handler
{
    public class RegistrarUsuarioHandler : IRequestHandler<RegistrarUsuarioCommand, Guid>
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IUsuarioService _usuarioService;

        public RegistrarUsuarioHandler(IUsuarioService usuarioService, IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
            _usuarioService = usuarioService;
        }

        public async Task<Guid> Handle(RegistrarUsuarioCommand request, CancellationToken cancellationToken)
        {
         
               var usuarioExiste = await _usuarioService.ExisteUsuarioMongoAsync(request.Usuario.Correo);
                if (usuarioExiste)
                {
                    throw new CorreoRegistradoException();
                }

                var usuario = UsuarioFactory.CrearUsuario(request.Usuario.Nombre,request.Usuario.Apellido,request.Usuario.Correo,request.Usuario.Contraseña,request.Usuario.Telefono,request.Usuario.Direccion);


                var usuarioId = await _usuarioService.RegistrarUsuarioPostgresAsync(usuario.ToPostgres());

                if (usuarioId!=Guid.Empty)
                {
                    await _publishEndpoint.Publish(new UsuarioRegistradoEvent(usuarioId, usuario.Nombre.nombre, usuario.Apellido.apellido, usuario.Correo.correo, usuario.Telefono.telefono, usuario.Direccion.direccion, 3));
                    await _usuarioService.RegistrarUsuarioKeycloakAsync(request.Usuario.Correo, request.Usuario.Nombre, request.Usuario.Apellido, request.Usuario.Contraseña);
                    return usuarioId;
                } else
                    return Guid.Empty;
            
        }

    }
}