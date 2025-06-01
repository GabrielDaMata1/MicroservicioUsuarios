namespace MicroservicioUsuarios.Application.Exceptions
{
    public class UsuarioVacioException: Exception
    {
        public UsuarioVacioException():base("Error, el usuario proporcionado se encuentra vacío") { }
    }
}
