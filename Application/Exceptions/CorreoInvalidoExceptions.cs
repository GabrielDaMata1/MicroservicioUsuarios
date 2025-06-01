namespace MicroservicioUsuarios.Application.Exceptions
{
    public class CorreoInvalidoException : Exception
    {
        public CorreoInvalidoException()
        : base("Error, formato de correo invalido.")
        {
        }
    }
}