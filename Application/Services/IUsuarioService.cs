using System.Net;
using MicroservicioUsuarios.Infrastructure.Models;

namespace MicroservicioUsuarios.Application.Services
{
    public interface IUsuarioService
    {
        Task<Guid> RegistrarUsuarioPostgresAsync(UsuarioPostgres usuario);
        Task<HttpStatusCode> RegistrarUsuarioMongoAsync(UsuarioMongo usuario);

        Task<HttpStatusCode> ActualizarUsuarioMongoAsync(string correo, UsuarioMongo usuario);
        Task<HttpStatusCode> ActualizarUsuarioPostgresAsync(string correo, UsuarioPostgres usuario);
        Task<Guid> ObtenerGuidPorCorreoMongoAsync(string correo);
        Task<int> ObtenerIdRolPorCorreoMongoAsync(string correo);
        Task<List<UsuarioMongo>> ObtenerTodosLosUsuariosAsync();
        Task<bool> ExisteUsuarioMongoAsync(string correo);
        Task<HttpStatusCode> RegistrarUsuarioKeycloakAsync(string email, string name, string lastname, string password);
        Task<UsuarioMongo> ObtenerUsuarioMongoPorCorreoAsync(string correo);
        Task<int> ObtenerIdRolPorNombreMongoAsync(string nombreRol);

        Task<HttpStatusCode> CambiarContrasenaKeycloakAsync(string userId, string nuevaContrasena);

        Task<HttpStatusCode> EnviarCorreoConfirmacionKeycloak(string userId, string redirectUri = null);

        Task<string> ObtenerRolUsuarioMongoAsync(string correo);

        Task<HttpStatusCode> ActualizarUsuarioEnKeycloakAsync(string userId, string nuevoNombre, string nuevoApellido,
            string nuevoCorreo);
    }
}
