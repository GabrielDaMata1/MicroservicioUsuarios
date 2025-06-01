using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MicroservicioUsuarios.Application.Querys;
using MicroserviciosUsuarios.Infrastructure.Repositories.MongoDB;
using MicroservicioUsuarios.Application.DTOs;
using MicroservicioUsuarios.Application.Services;
using MicroserviciosUsuarios.Domain.Entities;
using MicroservicioUsuarios.Application.Exceptions;

public class ConsultarHistorialActividadHandler : IRequestHandler<ConsultarHistorialActividadQuery, List<HistorialActividadDTO>>
{
    private readonly IUsuarioService _usuarioService;
    private readonly IHistorialActividadServices _historialActividadServices;


    public ConsultarHistorialActividadHandler(IUsuarioService usuarioService, IHistorialActividadServices historialActividadServices)
    {
        _usuarioService = usuarioService;
        _historialActividadServices = historialActividadServices;
    }

    public async Task<List<HistorialActividadDTO>> Handle(ConsultarHistorialActividadQuery request, CancellationToken cancellationToken)
    {
        var idUsuario = await _usuarioService.ObtenerGuidPorCorreoMongoAsync(request.CorreoUsuario);
        if (idUsuario == null || idUsuario == Guid.Empty)
        {
            throw new UsuarioNoEncontradoException();
        }
        
        var historial = await _historialActividadServices.ObtenerHistorialPorCorreoMongoAsync(idUsuario);
        if (historial == null || !historial.Any())
        {
            return new List<HistorialActividadDTO>();
        }
        return historial.Select(h => new HistorialActividadDTO(h.TipoAccion, h.FechaHora)).ToList();
    }
}