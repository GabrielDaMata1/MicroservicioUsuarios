using System.Collections.Generic;
using System.Threading.Tasks;

public interface IRolRepository
{
    Task<List<RolConPermisosDTO>> ObtenerRolesConPermisosAsync();

}