using MassTransit;
using System.Threading.Tasks;
using MongoDB.Bson;
using MicroserviciosUsuarios.Infrastructure.Repositories.MongoDB;
using MicroservicioUsuarios.Infrastructure.Models;
using MicroserviciosUsuarios.Domain.Events;
using MicroserviciosUsuarios.Domain.Entities;
using MicroservicioUsuarios.Application.Services;

namespace MicroserviciosUsuarios.Infrastructure.Consumers
{
    public class UsuarioRegistradoConsumer : IConsumer<UsuarioRegistradoEvent>
{
    private readonly IUsuarioService _usuarioService;

        public UsuarioRegistradoConsumer(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }
        public async Task Consume(ConsumeContext<UsuarioRegistradoEvent> context)
    {
        var usuarioMongo = new UsuarioMongo
        {
            Id = context.Message.UsuarioId,
            Nombre = context.Message.Nombre,
            Apellido = context.Message.Apellido,
            Correo = context.Message.Correo,
            Telefono = context.Message.Telefono,
            Direccion = context.Message.Direccion,
            RolId = context.Message.RolId
        };

        await _usuarioService.RegistrarUsuarioMongoAsync(usuarioMongo);
    }
}
}