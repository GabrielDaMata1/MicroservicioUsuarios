using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;

public interface IRolMongoRepository
{
    Task<List<RolConPermisosDTO>> ObtenerRolesConPermisosAsync();
    Task<List<int>> ObtenerIdsPermisosAsync(List<string> nombresPermisos);

    Task<bool> ModificarPermisosRolAsync(int rolId, List<int> nuevosPermisos);


}