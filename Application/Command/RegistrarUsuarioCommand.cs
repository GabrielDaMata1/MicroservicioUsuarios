using MediatR;
using MicroservicioUsuarios.Application.DTOs;


namespace MicroservicioUsuarios.Application.Command
{
    public class RegistrarUsuarioCommand : IRequest<Guid>
    {
        public UsuarioRegistroDTO Usuario { get; set; }
        public RegistrarUsuarioCommand(UsuarioRegistroDTO usuario)
        {
            Usuario = usuario;
        }

    }
}
