using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MicroservicioUsuarios.Application.Services;

public class ConsultarRolesYPermisosHandler : IRequestHandler<ConsultarRolesYPermisosQuery, List<RolConPermisosDTO>>
{
    private readonly IUsuarioService _usuarioService;

    public ConsultarRolesYPermisosHandler(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    public async Task<List<RolConPermisosDTO>> Handle(ConsultarRolesYPermisosQuery request, CancellationToken cancellationToken)
    {
        var resultado = await _usuarioService.ObtenerRolesConPermisosMongoAsync();

        if (resultado == null || resultado.Count == 0)
        {
            return new List<RolConPermisosDTO>();
        }

        return resultado;
    }
}