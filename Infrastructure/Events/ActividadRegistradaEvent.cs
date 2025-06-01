using System;
namespace MicroserviciosUsuarios.Infrastructure.Events
{
    public record ActividadRegistradaEvent(Guid IdActividad, Guid UsuarioID, string TipoActividad, DateTime fecha);
}
