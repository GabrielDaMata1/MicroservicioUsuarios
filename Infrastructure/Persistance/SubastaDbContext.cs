using Microsoft.EntityFrameworkCore;
using MicroservicioUsuarios.Infrastructure.Models;
namespace MicroserviciosUsuarios.Infrastructure.Persistance
{
    public class SubastaDbContext : DbContext
    {
        public SubastaDbContext(DbContextOptions<SubastaDbContext> options) : base(options) { }

        public DbSet<UsuarioPostgres> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Permiso> Permisos { get; set; }
        public DbSet<RolPermisos> RolesPermisos { get; set; }
        public DbSet<HistorialActividad> Historial_Actividad { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UsuarioPostgres>()
                .HasIndex(u => u.Correo)
                .IsUnique();
            modelBuilder.Entity<RolPermisos>()
                .HasKey(rp => new { rp.RolId, rp.PermisoId });

            modelBuilder.Entity<RolPermisos>()
                .HasOne(rp => rp.Rol)
                .WithMany()
                .HasForeignKey(rp => rp.RolId);

            modelBuilder.Entity<RolPermisos>()
                .HasOne(rp => rp.Permiso)
                .WithMany()
                .HasForeignKey(rp => rp.PermisoId);

            base.OnModelCreating(modelBuilder);

        }
    }

}
