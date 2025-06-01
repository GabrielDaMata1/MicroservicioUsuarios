namespace MicroservicioUsuarios.Application.Exceptions
{
    public class NombreRolInvalidoException : Exception
    {
        public NombreRolInvalidoException() : base("El nombre de rol no puede ser nulo o vacío.") { }
        public NombreRolInvalidoException(string message) : base(message) { }
    }
}
