using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MicroservicioUsuarios.Infrastructure.Models
{
    public class RolPermisos
    {
        [ForeignKey("RolId")]
        public int RolId { get; set; }
        public virtual Rol Rol { get; set; }
        [ForeignKey("PermisoId")]
        public int PermisoId { get; set; }
        public virtual Permiso Permiso { get; set; }
    }
}
