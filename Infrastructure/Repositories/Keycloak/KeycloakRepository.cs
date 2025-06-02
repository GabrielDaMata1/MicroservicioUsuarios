using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MicroservicioUsuarios.Infrastructure.Services;
using MicroserviciosUsuarios.Infrastructure.Repositories.Keycloak;
using System.Xml.Linq;

namespace MicroserviciosUsuarios.Infrastructure.Repositories.Keycloak
{

    public class KeycloakRepository : IKeycloakRepository
    {
        private readonly KeycloakAuthService _authService;

        public KeycloakRepository(KeycloakAuthService authService)
        {
            _authService = authService;
        }

        public async Task<HttpStatusCode> CambiarContrasenaAsync(string userId, string nuevaContrasena)
        {
            try
            {
                await _authService.CambiarContrasenaAsync(userId, nuevaContrasena);
                return HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                return HttpStatusCode.RequestTimeout;
            }
        }

        public async Task<HttpStatusCode> RegistrarUsuarioAsync(string email, string name, string lastname, string password)
        {
            try
            {
               await _authService.CreateUserAsync(email, name,lastname, password);
               return HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                return HttpStatusCode.RequestTimeout; 
            }
        }

        public async Task<HttpStatusCode> EnviarCorreoVerificacion(string userId, string redirectUri = null)
        {
            try
            {
                await _authService.EnviarCorreoVerificacionAsync(userId, redirectUri);
                return HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                return HttpStatusCode.RequestTimeout;
            }
        }

        public async Task<HttpStatusCode> ActualizarUsuarioEnKeycloakAsync(string userId, string nuevoNombre, string nuevoApellido, string nuevoCorreo)
        {
            try
            {
                await _authService.ActualizarUsuarioEnKeycloakAsync(userId, nuevoNombre, nuevoApellido, nuevoCorreo);
                return HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                return HttpStatusCode.RequestTimeout;
            }
        }

        public async Task<HttpStatusCode> AsignarRolUsuario(string userId, string roleName)
        {
            try
            {
                await _authService.AsignarRolUsuario(userId, roleName);
                return HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                return HttpStatusCode.RequestTimeout;
            }
        }

    }

}