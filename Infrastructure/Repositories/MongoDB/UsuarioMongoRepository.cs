using MicroservicioUsuarios.Infrastructure.Models;
using MongoDB.Driver;
using MongoDB.Driver.Core;
using System.Threading.Tasks;
using MicroservicioUsuarios.Application.Exceptions;
using System.Net;

namespace MicroserviciosUsuarios.Infrastructure.Repositories.MongoDB
{
    public class UsuarioMongoRepository : IUsuarioMongoRepository
    {
        private readonly IMongoCollection<UsuarioMongo> _usuariosCollection;
        private readonly IMongoCollection<RolMongo> _rolesCollection;

        public UsuarioMongoRepository(IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase("SubastasDB");
            _usuariosCollection = database.GetCollection<UsuarioMongo>("Usuarios");
            _rolesCollection = database.GetCollection<RolMongo>("Roles");
        }

        public async Task<HttpStatusCode> RegistrarUsuarioAsync(UsuarioMongo usuario)
        {
            if (usuario == null)
                throw new UsuarioVacioException();
            try
            {
                 _usuariosCollection.InsertOneAsync(usuario);
                 return HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                throw new UsuarioMongoRepositoryException($"Error al intentar registrar el usuario en MongoDB: {ex.Message}", ex);
            }
        }
        public async Task<string> ObtenerRolUsuarioAsync(string correo)
        {
            try
            {
                var usuario = await _usuariosCollection.Find(u => u.Correo.Equals(correo)).FirstOrDefaultAsync();
                if (usuario == null)
                {
                    throw new UsuarioNoEncontradoException();
                }
                var rol = await _rolesCollection.Find(r => r.Id == usuario.RolId).FirstOrDefaultAsync();
                return rol?.Nombre;
            }
            catch (UsuarioNoEncontradoException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UsuarioMongoRepositoryException($"Error al obtener el rol del usuario con correo '{correo}': {ex.Message}", ex);
            }
        }

        public async Task<UsuarioMongo> ObtenerUsuarioPorCorreoAsync(string correo)
        {
            return await _usuariosCollection.Find(u => u.Correo == correo).FirstOrDefaultAsync();
        }

        public async Task<Guid> ObtenerGuidPorCorreoAsync(string correo)
        {
            var usuario = await _usuariosCollection.Find(u => u.Correo == correo).FirstOrDefaultAsync();
            if (usuario == null)
                throw new UsuarioNoEncontradoException();
            else
               return usuario.Id;
        }

        public async Task<HttpStatusCode> ActualizarUsuarioAsync(UsuarioMongo usuario, string correo)
        {
            var resultado = await _usuariosCollection.ReplaceOneAsync(
                u => u.Correo == correo, usuario);
            return HttpStatusCode.OK;
        }

        public async Task<int> ObtenerIdRolPorCorreoAsync(string correo)
        {
            var usuario = await _usuariosCollection.Find(u => u.Correo == correo).FirstOrDefaultAsync();
            return usuario.RolId;
        }

        public async Task<bool> ExisteUsuarioAsync(string correo)
        {

            var filter = Builders<UsuarioMongo>.Filter.Eq(user => user.Correo, correo);
            long count = await _usuariosCollection.CountDocumentsAsync(filter);

            return count > 0;
        }

        public async Task<List<UsuarioMongo>> ObtenerTodosLosUsuariosAsync()
        {
            return await _usuariosCollection.Find(_ => true).ToListAsync();
        }

        public async Task<int> ObtenerIdRolPorNombreAsync(string nombreRol)
        {
            var filtro = Builders<RolMongo>.Filter.Eq(r => r.Nombre, nombreRol);
            var rol = await _rolesCollection.Find(filtro).FirstOrDefaultAsync();

            return rol.Id;
        }


    }
}