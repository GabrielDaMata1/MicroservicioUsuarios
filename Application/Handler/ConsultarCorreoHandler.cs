using MediatR;
using System.Threading;
using System.Threading.Tasks;
using MicroserviciosUsuarios.Infrastructure.Repositories.MongoDB;
using MicroservicioUsuarios.Application.Querys;
using MicroservicioUsuarios.Application.Exceptions;
using MicroservicioUsuarios.Application.Services;
using MicroservicioUsuarios.Infrastructure.Models;

namespace MicroserviciosUsuarios.Application.Handler
{

    public class ConsultarCorreoHandler : IRequestHandler<ConsultarCorreoQuery, UsuarioMongo>
{
    private readonly IUsuarioService _usuarioService;

        public ConsultarCorreoHandler(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    public async Task<UsuarioMongo> Handle(ConsultarCorreoQuery request, CancellationToken cancellationToken)
    {
        var usuario = await _usuarioService.ObtenerGuidPorCorreoMongoAsync(request.Correo.Correo);
 
        if (usuario == null || usuario==Guid.Empty)
        {
            throw new UsuarioNoEncontradoException();
        }
        else
            return await _usuarioService.ObtenerUsuarioMongoPorCorreoAsync(request.Correo.Correo);
    }
 }
}