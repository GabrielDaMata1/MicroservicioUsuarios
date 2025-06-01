using MediatR;
using System;
using MicroservicioUsuarios.Application.DTOs;

namespace MicroservicioUsuarios.Application.Command
{
    public class ActualizarPerfilUsuarioCommand : IRequest<bool>
{
    public ActualizarPerfilDTO actualizarPerfilDTO;
    public string correo;
    public ActualizarPerfilUsuarioCommand(ActualizarPerfilDTO actualizarPerfilDTO, string correo)
        {
            this.actualizarPerfilDTO = actualizarPerfilDTO;
            this.correo = correo;
            
        }
    }
}