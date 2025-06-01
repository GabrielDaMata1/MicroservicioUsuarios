using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
namespace MicroservicioUsuarios.Infrastructure.Models
{
    public class HistorialActividad
{
    [Key]
    public Guid Id { get; set; } 
    public string TipoAccion { get; set; }
    public DateTime FechaHora { get; set; }
    [ForeignKey("UsuarioId")]
    public Guid UsuarioId { get; set; }
    public virtual UsuarioPostgres Usuario { get; set; }
    }
}