using System.Net;
using System.Xml.Linq;
using MassTransit;
using MicroserviciosUsuarios.Domain.Entities;
using MicroserviciosUsuarios.Infrastructure.Repositories.Keycloak;
using MicroserviciosUsuarios.Infrastructure.Repositories.MongoDB;
using MicroserviciosUsuarios.Infrastructure.Repositories.PostgreSQL;
using MicroservicioUsuarios.Application.Exceptions;
using MicroservicioUsuarios.Infrastructure.Models;
using MongoDB.Driver;

namespace MicroservicioUsuarios.Application.Services
{
    public class UsuarioService: IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUsuarioMongoRepository _usuarioMongoRepository;
        private readonly IKeycloakRepository _usuarioKeycloakRepository;
        private readonly IRolRepository _rolRepository;


    public UsuarioService(IUsuarioRepository usuarioRepository, IUsuarioMongoRepository usuarioMongoRepository, IKeycloakRepository usuarioKeycloakRepository, IRolRepository rolRepository)

    {
        _usuarioRepository = usuarioRepository;
        _usuarioMongoRepository = usuarioMongoRepository;
        _usuarioKeycloakRepository= usuarioKeycloakRepository;
        _rolRepository = rolRepository;

    }

        public async Task<Guid> RegistrarUsuarioPostgresAsync(UsuarioPostgres usuario)
        {
            if (usuario == null)
                throw new UsuarioVacioException();
            try
            {
                var resul= await _usuarioRepository.RegistrarUsuarioAsync(usuario);
                return resul;
            }
            catch (Exception ex)
            {
                throw new UsuarioPostgresRepositoryException($"Error al intentar registrar el usuario en PostgreSQL: {ex.Message}", ex);
            }
        }

        public async Task<HttpStatusCode> ActualizarUsuarioPostgresAsync(string correo, UsuarioPostgres usuario)
        {
            if (usuario == null)
                throw new UsuarioVacioException();
            try
            {
                var resul = await _usuarioRepository.ActualizarUsuarioAsync(correo, usuario);
                return resul;
            }
            catch (Exception ex)
            {
                throw new UsuarioPostgresRepositoryException($"Error al intentar actualizar el usuario en PostgreSQL: {ex.Message}", ex);
            }
        }

        public async Task<HttpStatusCode> RegistrarUsuarioMongoAsync(UsuarioMongo usuario)
        {
            if (usuario == null)
                throw new UsuarioVacioException();
            try
            {
                var resul = await _usuarioMongoRepository.RegistrarUsuarioAsync(usuario);
                return resul;
            }
            catch (Exception ex)
            {
                throw new UsuarioMongoRepositoryException($"Error al intentar registrar el usuario en PostgreSQL: {ex.Message}", ex);
            }
        }

        public async Task<HttpStatusCode> ActualizarUsuarioMongoAsync(string correo, UsuarioMongo usuario)
        {
            if (usuario == null)
                throw new UsuarioVacioException();
            try
            {
                var resul = await _usuarioMongoRepository.ActualizarUsuarioAsync(usuario, correo);
                return resul;
            }
            catch (Exception ex)
            {
                throw new UsuarioMongoRepositoryException($"Error al intentar actualizar el usuario en PostgreSQL: {ex.Message}", ex);
            }
        }

        public async Task<Guid> ObtenerGuidPorCorreoMongoAsync(string correo)
        {
            if (correo == null)
                throw new CorreoInvalidoException();
            try
            {
                var resul = await _usuarioMongoRepository.ObtenerGuidPorCorreoAsync(correo);
                return resul;
            }
            catch (Exception ex)
            {
                throw new UsuarioMongoRepositoryException($"Error al intentar obtener el usuario en MongoDB: {ex.Message}", ex);
            }
        }

        public async Task<int> ObtenerIdRolPorCorreoMongoAsync(string correo)
        {
            if ((string.IsNullOrWhiteSpace(correo)))
                throw new CorreoInvalidoException();
            try
            {
                    var resul = await _usuarioMongoRepository.ObtenerIdRolPorCorreoAsync(correo);
                    return resul;
            }
            catch (Exception ex)
            {
                    throw new UsuarioMongoRepositoryException($"Error al intentar obtener el usuario en MongoDB: {ex.Message}", ex);
            }
        }

        public async Task<bool> ExisteUsuarioMongoAsync(string correo)
        {
            if (string.IsNullOrWhiteSpace(correo))
                throw new CorreoInvalidoException();

            try
            {
                var resul = await _usuarioMongoRepository.ExisteUsuarioAsync(correo);
                return resul;
            }
            catch (Exception ex)
            {
                throw new UsuarioMongoRepositoryException($"Error al intentar verificar la existencia del usuario en MongoDB: {ex.Message}", ex);
            }
        }

        public async Task<HttpStatusCode> RegistrarUsuarioKeycloakAsync(string email, string name, string lastname, string password)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new CorreoInvalidoException();

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(lastname) || string.IsNullOrWhiteSpace(password))
                throw new DatosUsuarioKeycloakErroneosException();

            try
            {
                var resul = await _usuarioKeycloakRepository.RegistrarUsuarioAsync(email, name,lastname, password);
                return resul;
            }
            catch (Exception ex)
            {
                throw new UsuarioMongoRepositoryException($"Error al intentar registrar el usuario en Keycloak: {ex.Message}", ex);
            }
        }
        public async Task<List<UsuarioMongo>> ObtenerTodosLosUsuariosAsync()
        {
            try
            {
                var resul = await _usuarioMongoRepository.ObtenerTodosLosUsuariosAsync();
                return resul;
            }
            catch (Exception ex)
            {
                throw new UsuarioMongoRepositoryException($"Error al intentar obtener los usuarios en Keycloak: {ex.Message}", ex);
            }
        }

        public async Task<UsuarioMongo> ObtenerUsuarioMongoPorCorreoAsync(string correo)
        {
            if (string.IsNullOrWhiteSpace(correo))
                throw new CorreoInvalidoException();
            try
            {
                var resul = await _usuarioMongoRepository.ObtenerUsuarioPorCorreoAsync(correo);
                return resul;
            }
            catch (Exception ex)
            {
                throw new UsuarioMongoRepositoryException($"Error al intentar obtener el usuario en MongoDB: {ex.Message}", ex);
            }
        }

        public async Task<int> ObtenerIdRolPorNombreMongoAsync(string nombreRol)
        {
            if (string.IsNullOrWhiteSpace(nombreRol))
                throw new NombreRolInvalidoException();
            try
            {
                var resul = await _usuarioMongoRepository.ObtenerIdRolPorNombreAsync(nombreRol);
                return resul;
            }
            catch (Exception ex)
            {
                throw new UsuarioMongoRepositoryException($"Error al intentar obtener el usuario en MongoDB: {ex.Message}", ex);
            }
        }

        public async Task<HttpStatusCode> CambiarContrasenaKeycloakAsync(string userId, string nuevaContrasena)
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new DatosUsuarioKeycloakErroneosException();

            if (string.IsNullOrWhiteSpace(nuevaContrasena))
                throw new ContraseñaInvalidaException();
            try
            {
                var resul= await _usuarioKeycloakRepository.CambiarContrasenaAsync(userId,  nuevaContrasena);
                return resul;
            }
            catch (Exception ex)
            {
                throw new KeycloakIntegrationException($"Error al intentar cambiar la contraseña en Keycloak: {ex.Message}", ex);

            }
        }

        public async Task<HttpStatusCode> EnviarCorreoConfirmacionKeycloak(string userId, string redirectUri = null)
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new DatosUsuarioKeycloakErroneosException();

            try
            {
                var resul = await _usuarioKeycloakRepository.EnviarCorreoVerificacion(userId, redirectUri);
                return resul;
            }
            catch (Exception ex)
            {
                throw new KeycloakIntegrationException($"Error al intentar enviar el correo en Keycloak: {ex.Message}", ex);

            }
        }

        public async Task<string> ObtenerRolUsuarioMongoAsync(string correo)
        {
            if (string.IsNullOrWhiteSpace(correo))
                throw new CorreoInvalidoException();

            try
            {
                var resul = await _usuarioMongoRepository.ObtenerRolUsuarioAsync(correo);
                return resul;
            }
            catch (Exception ex)
            {
                throw new UsuarioMongoRepositoryException();

            }
        }

        public async Task<HttpStatusCode> ActualizarUsuarioEnKeycloakAsync(string userId, string nuevoNombre, string nuevoApellido, string nuevoCorreo)
        {
            try
            {
                await _usuarioKeycloakRepository.ActualizarUsuarioEnKeycloakAsync(userId, nuevoNombre, nuevoApellido, nuevoCorreo);
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
                await _usuarioKeycloakRepository.AsignarRolUsuario(userId, roleName);
                return HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                return HttpStatusCode.RequestTimeout;
            }
        }

        public async Task<List<RolConPermisosDTO>> ObtenerRolesConPermisosMongoAsync()
        {
            try
            {
                var resul = await _rolRepository.ObtenerRolesConPermisosAsync();
                return resul;
            }
            catch (Exception ex)
            {
                throw new UsuarioMongoRepositoryException($"Error al intentar obtener el usuario en MongoDB: {ex.Message}", ex);
            }
        }



    }

}