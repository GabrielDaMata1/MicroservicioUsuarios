using MediatR;
using MicroservicioUsuarios.Application.DTOs;
using System.Collections.Generic;

namespace MicroservicioUsuarios.Application.Querys
{
    public class ConsultarUsuariosQuery : IRequest<List<ObtenerUsuariosDTO>>

    {
    }
}
