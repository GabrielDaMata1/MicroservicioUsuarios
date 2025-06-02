namespace MicroservicioUsuarios.Infrastructure.Repositories.PostgreSQL
{
    public interface IRolRepository
    {
        Task<bool> ModificarPermisosRolAsync(int rolId, List<int> nuevosPermisos);

    }
}
