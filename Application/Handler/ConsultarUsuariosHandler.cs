using MassTransit;
using MediatR;
using MicroservicioUsuarios.Application.DTOs;
using MicroservicioUsuarios.Application.Querys;
using MicroservicioUsuarios.Application.Services;

namespace MicroservicioUsuarios.Application.Handler
{
    public class ConsultarUsuariosHandler : IRequestHandler<ConsultarUsuariosQuery, List<ObtenerUsuariosDTO>>

    {
        private readonly IUsuarioService _usuarioService;
        public ConsultarUsuariosHandler(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }
        public async Task<List<ObtenerUsuariosDTO>> Handle(ConsultarUsuariosQuery request, CancellationToken cancellationToken)
        {
            var usuarios = await _usuarioService.ObtenerTodosLosUsuariosAsync();
            if (usuarios == null)
            {
                return new List<ObtenerUsuariosDTO>();
            }
            var obtenerUsuariosTasks = usuarios.Select(async u =>
            {
                var nombreRol = await _usuarioService.ObtenerRolUsuarioMongoAsync(u.Correo);
                return new ObtenerUsuariosDTO(u.Nombre, u.Apellido, u.Correo, u.Telefono, u.Direccion, nombreRol);
            }).ToList();

            var usuariosConRoles = await Task.WhenAll(obtenerUsuariosTasks);

            return usuariosConRoles.ToList();
        }

    }
}
