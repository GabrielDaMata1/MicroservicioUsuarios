using MediatR;
using MicroservicioUsuarios.Application.DTOs;

namespace MicroservicioUsuarios.Application.Command
{
    public class AsignarRolCommand : IRequest<bool>
    {
        public string CorreoUsuario { get; set; }
        public AsignarRolDTO NombreRol { get; set; }

        public AsignarRolCommand(string correoUsuario, AsignarRolDTO nombreRol)
        {
            CorreoUsuario = correoUsuario;
            NombreRol = nombreRol;
        }
    }


}
