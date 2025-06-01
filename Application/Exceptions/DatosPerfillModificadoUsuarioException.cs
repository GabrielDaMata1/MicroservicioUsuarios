namespace MicroservicioUsuarios.Application.Exceptions
{
    public class DatosPerfillModificadoUsuarioException: Exception
    {
        public DatosPerfillModificadoUsuarioException()
            : base("Error,datos de usuario invalidos")
        {
        }
    }
}
