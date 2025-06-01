using System.Net;
using MicroservicioUsuarios.Infrastructure.Models;
namespace MicroserviciosUsuarios.Infrastructure.Repositories.MongoDB
{
    public interface IUsuarioMongoRepository
    {
        Task<HttpStatusCode> RegistrarUsuarioAsync(UsuarioMongo usuario);
        Task<UsuarioMongo> ObtenerUsuarioPorCorreoAsync(string correo);
        Task<Guid> ObtenerGuidPorCorreoAsync(string correo);
        Task<HttpStatusCode> ActualizarUsuarioAsync(UsuarioMongo usuario, string correo);
        Task<int> ObtenerIdRolPorCorreoAsync(string correo);
        Task<List<UsuarioMongo>> ObtenerTodosLosUsuariosAsync();
        Task<bool> ExisteUsuarioAsync(string correo);

        Task<int> ObtenerIdRolPorNombreAsync(string nombreRol);

        Task<string> ObtenerRolUsuarioAsync(string correo);


    }
}