namespace MicroserviciosUsuarios.Domain.Events
{
    public record UsuarioRegistradoEvent(Guid UsuarioId, string Nombre, string Apellido, string Correo, string Telefono, string Direccion, int RolId);
}
