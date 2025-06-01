namespace MicroservicioUsuarios.Application.Exceptions
{
    public class HistorialVacioException: Exception
    {
        public HistorialVacioException() : base("Error, el historial de actividades proporcionado se encuentra vacío") { }
    }
}
