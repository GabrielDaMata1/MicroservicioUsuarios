namespace MicroserviciosUsuarios.Domain.Events
{
    public record UsuarioModificadoEvent(Guid UsuarioId, string Nombre, string Apellido, string Correo, string Telefono, string Direccion, int RolId, string correoActualUsuario);
}
