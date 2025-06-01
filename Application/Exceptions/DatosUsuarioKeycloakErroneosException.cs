namespace MicroservicioUsuarios.Application.Exceptions
{
    public class DatosUsuarioKeycloakErroneosException: Exception
    {
        public DatosUsuarioKeycloakErroneosException()
            : base("Error, datos del usuario en keycloak erróneos .")
        {
        }
    }
}
