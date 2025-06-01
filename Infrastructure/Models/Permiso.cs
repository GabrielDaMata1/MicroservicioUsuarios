using System;
using System.ComponentModel.DataAnnotations;
namespace MicroservicioUsuarios.Infrastructure.Models
{
    public class Permiso
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
}
