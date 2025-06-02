using MassTransit;
using MicroserviciosUsuarios.Domain.Events;
using MicroservicioUsuarios.Application.Services;
using MicroservicioUsuarios.Domain.Events;
using MicroservicioUsuarios.Infrastructure.Models;

namespace MicroservicioUsuarios.Infrastructure.Consumers
{
    public class RolPermisosModificadoConsumer : IConsumer<RolPermisosModificadoEvent>
    {
        private readonly IUsuarioService _usuarioService;

        public RolPermisosModificadoConsumer(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        public async Task Consume(ConsumeContext<RolPermisosModificadoEvent> context)
        {
            await _usuarioService.ModificarPermisosMongoRolAsync(context.Message.rolId, context.Message.nuevosPermisos);
        }
    }
}
