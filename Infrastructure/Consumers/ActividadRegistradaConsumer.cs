using MassTransit;
using System.Threading.Tasks;
using MongoDB.Bson;
using MicroserviciosUsuarios.Infrastructure.Repositories.MongoDB;
using MicroservicioUsuarios.Infrastructure.Models;
using MicroserviciosUsuarios.Infrastructure.Events;
using MicroserviciosUsuarios.Domain.Entities;
using MicroservicioUsuarios.Application.Services;

namespace MicroserviciosUsuarios.Infrastructure.Consumers
{
    public class ActividadRegistradaConsumer : IConsumer<ActividadRegistradaEvent>
{
    private readonly IHistorialActividadServices _actividadServices;

		public ActividadRegistradaConsumer(IHistorialActividadServices actividadServices)
    {
            _actividadServices = actividadServices;
    }

    public async Task Consume(ConsumeContext<ActividadRegistradaEvent> context)
    {
        var HistorialActividadMongo = new HistorialActividadMongo
		{
            Id = context.Message.IdActividad,
            TipoAccion = context.Message.TipoActividad,
            FechaHora = context.Message.fecha,
            UsuarioId = context.Message.UsuarioID
        };

        await _actividadServices.RegistrarActividadMongoAsync(HistorialActividadMongo);
    }
}
}