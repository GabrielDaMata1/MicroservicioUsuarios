namespace MicroservicioUsuarios.Application.Exceptions
{
    public class UsuarioNoEncontradoException: Exception
    {
      public UsuarioNoEncontradoException()
      :base("Error, usuario no encontrado.")
        {
        }
    }
}
