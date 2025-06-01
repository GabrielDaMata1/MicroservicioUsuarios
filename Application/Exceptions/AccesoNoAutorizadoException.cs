namespace MicroservicioUsuarios.Application.Exceptions
{
    public class AccesoNoAutorizadoException : Exception
{
    public AccesoNoAutorizadoException()
    : base("Error, acceso no autorizado")
    {
    }
}
}