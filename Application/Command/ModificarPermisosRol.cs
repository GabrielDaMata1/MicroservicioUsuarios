using MediatR;
using System.Collections.Generic;
using MicroservicioUsuarios.Application.DTOs;

namespace MicroservicioUsuarios.Application.Command
{

    public class ModificarPermisosRolCommand : IRequest<bool>
    {

        public ModificarPermisosRolDTO PermisosRolDto;
        public ModificarPermisosRolCommand(ModificarPermisosRolDTO PermisosRolDto)
        {
           this.PermisosRolDto=PermisosRolDto;
        }
    }


}
