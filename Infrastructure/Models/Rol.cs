using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MicroservicioUsuarios.Infrastructure.Models
{
    public class Rol
    {
        [Key]
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }
    }
}
