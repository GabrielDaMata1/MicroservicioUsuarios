using MediatR;
using System.Collections.Generic;
using MicroservicioUsuarios.Application.DTOs;
namespace MicroservicioUsuarios.Application.Querys
{
    public class ConsultarHistorialActividadQuery : IRequest<List<HistorialActividadDTO>>
{                       
    public string CorreoUsuario { get; set; }

    public ConsultarHistorialActividadQuery(string correoUsuario)
    {
        CorreoUsuario = correoUsuario;
    }
}
}