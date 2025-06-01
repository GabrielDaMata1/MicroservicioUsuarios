using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace MicroservicioUsuarios.Infrastructure.Models
{
    public class UsuarioPostgres
    {
        [Key]
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        [ForeignKey("RolId")]
        public int RolId { get; set; }
        public virtual Rol Rol { get; set; }


    }

}
