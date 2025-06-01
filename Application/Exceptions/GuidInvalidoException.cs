namespace MicroservicioUsuarios.Application.Exceptions
{
    public class GuidInvalidoException: Exception
    {
        public GuidInvalidoException() : base("Error, Guid invalido") { }
    }
}
