using Microsoft.EntityFrameworkCore;
using System;
using System.Net;
using System.Threading.Tasks;
using MicroserviciosUsuarios.Infrastructure.Persistance;
using MicroservicioUsuarios.Infrastructure.Models;
namespace MicroserviciosUsuarios.Infrastructure.Repositories.PostgreSQL
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly SubastaDbContext _dbContext;

        public UsuarioRepository(SubastaDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> RegistrarUsuarioAsync(UsuarioPostgres usuario)
        {
            await _dbContext.Usuarios.AddAsync(usuario);
            await _dbContext.SaveChangesAsync();
            return usuario.Id;
        }
        public async Task<HttpStatusCode> ActualizarUsuarioAsync(string correo, UsuarioPostgres usuarioActualizado)
        {
            var usuario = await _dbContext.Set<UsuarioPostgres>()
                .FirstOrDefaultAsync(u => u.Correo ==correo);

            if (usuario == null) 
                return HttpStatusCode.NotFound; 

            usuario.Nombre = usuarioActualizado.Nombre ?? usuario.Nombre;
            usuario.Apellido=usuarioActualizado.Apellido;
            usuario.Correo = usuarioActualizado.Correo ?? usuario.Nombre;
            usuario.Telefono = usuarioActualizado.Telefono ?? usuario.Telefono;
            usuario.Direccion = usuarioActualizado.Direccion ?? usuario.Direccion;
            usuario.RolId = usuarioActualizado.RolId;

            _dbContext.Set<UsuarioPostgres>().Update(usuario);
             await _dbContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }



    }
}