using MediatR;
using MicroservicioUsuarios.Application.DTOs;
using MicroservicioUsuarios.Infrastructure.Models;
namespace MicroservicioUsuarios.Application.Querys
{
    public class ConsultarCorreoQuery : IRequest<UsuarioMongo>
{
    public ConsultarCorreoDTO Correo { get; set; }

    public ConsultarCorreoQuery(ConsultarCorreoDTO correo)
    {
        Correo = correo;
    }
}
}
