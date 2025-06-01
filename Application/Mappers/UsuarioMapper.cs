using MicroserviciosUsuarios.Domain.Entities;
using MicroservicioUsuarios.Infrastructure.Models;
namespace MicroserviciosUsuarios.Application.Mappers
{
	public static class UsuarioMapper
{
	public static UsuarioPostgres ToPostgres(this Usuario usuario)
	{
		return new UsuarioPostgres
		{
			Id = usuario.Id,
			Nombre = usuario.Nombre.nombre,
			Apellido = usuario.Apellido.apellido,
			Correo = usuario.Correo.correo,
			Telefono = usuario.Telefono.telefono,
			Direccion = usuario.Direccion.direccion,
			RolId =  3
		};
	}

    public static UsuarioPostgres FromMongoToPostgres(this UsuarioMongo usuario)
    {
        return new UsuarioPostgres
        {
            Id = usuario.Id,
            Nombre = usuario.Nombre,
            Apellido = usuario.Apellido,
            Correo = usuario.Correo,
            Telefono = usuario.Telefono,
            Direccion = usuario.Direccion,
            RolId = usuario.RolId
        };
    }
    }
}
