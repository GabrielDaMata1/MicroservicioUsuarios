using System.Net;

namespace MicroserviciosUsuarios.Infrastructure.Repositories.Keycloak
{
    public interface IKeycloakRepository
    { 
    Task<HttpStatusCode> ActualizarUsuarioEnKeycloakAsync(string userId, string nuevoNombre, string nuevoApellido, string nuevoCorreo);
    Task<HttpStatusCode> RegistrarUsuarioAsync(string email, string name, string lastname, string password);
    Task<HttpStatusCode> CambiarContrasenaAsync(string userId, string nuevaContrasena);

    Task<HttpStatusCode> EnviarCorreoVerificacion(string userId, string redirectUri = null);
}
}