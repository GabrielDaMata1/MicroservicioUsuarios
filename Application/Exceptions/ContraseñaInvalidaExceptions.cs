namespace MicroservicioUsuarios.Application.Exceptions
{
    public class ContraseñaInvalidaException : Exception
    {
        public ContraseñaInvalidaException()
        : base("Error, formato de contraseña invalida")
        {
        }
    }
}