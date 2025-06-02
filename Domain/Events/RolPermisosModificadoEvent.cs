namespace MicroservicioUsuarios.Domain.Events
{
    public record RolPermisosModificadoEvent(int rolId, List<int> nuevosPermisos);

}
