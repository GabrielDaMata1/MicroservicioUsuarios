using System;
namespace MicroservicioUsuarios.Application.DTOs
{
    public class HistorialActividadDTO
{
    public string TipoActividad { get; set; }
    public DateTime Fecha { get; set; }

    public HistorialActividadDTO(string tipoActividad, DateTime fecha)
    {
        TipoActividad = tipoActividad;
        Fecha = fecha;
    }
}
}