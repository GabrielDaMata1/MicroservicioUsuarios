using MediatR;
using MicroservicioUsuarios.Application.DTOs;
using System;
namespace MicroservicioUsuarios.Application.Command
{
    public class ActualizarContraseñaCommand : IRequest<bool>
{
    public ContraseñaNuevaDTO contraseñaDTO;
    public string Correo;

    public ActualizarContraseñaCommand(ContraseñaNuevaDTO contraseñaDTO, string correo)
    {
        this.contraseñaDTO = contraseñaDTO;
        this.Correo = correo;
    }
}
}