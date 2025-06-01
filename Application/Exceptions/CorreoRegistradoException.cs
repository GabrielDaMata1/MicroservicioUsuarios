namespace MicroservicioUsuarios.Application.Exceptions
{
    public class CorreoRegistradoException : Exception
    {
        public CorreoRegistradoException()
        : base("Error, Correo ya registrado")
        {
        }
    }
}