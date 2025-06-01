using System.Net;
using MicroservicioUsuarios.Infrastructure.Models;

namespace MicroserviciosUsuarios.Infrastructure.Repositories.PostgreSQL
{
	public interface IUsuarioRepository
{
        Task<Guid> RegistrarUsuarioAsync(UsuarioPostgres usuario);
        Task<HttpStatusCode> ActualizarUsuarioAsync(string correo, UsuarioPostgres usuario);
    }
}