namespace MicroservicioUsuarios.Application.Exceptions
{
    public class UsuarioPostgresRepositoryException: Exception
    {
        public UsuarioPostgresRepositoryException() { }
        public UsuarioPostgresRepositoryException(string message, Exception innerException) : base("message", innerException) { }
    }
}
