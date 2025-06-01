using System;
using MicroservicioUsuarios.Domain.Value_Object;

namespace MicroserviciosUsuarios.Domain.Entities
{
    public class Usuario
    {
        public Guid Id { get; private set; }
        public UsuarioNombre Nombre { get; private set; }
        public UsuarioApellido Apellido { get; private set; }
        public UsuarioCorreo Correo { get; private set; }
        public UsuarioContraseña Contraseña { get; private set; }
        public UsuarioTelefono Telefono { get; private set; }
        public UsuarioDireccion Direccion { get; private set; }

        public Usuario(UsuarioNombre nombre, UsuarioApellido apellido, UsuarioCorreo correo, UsuarioContraseña contraseña, UsuarioTelefono telefono, UsuarioDireccion direccion)
        {
            Id = Guid.NewGuid();
            Nombre = nombre;
            Apellido = apellido;
            Correo = correo;
            Contraseña = contraseña;
            Telefono = telefono;
            Direccion = direccion;
        }

        public Usuario(Guid Id, UsuarioNombre nombre, UsuarioApellido apellido, UsuarioCorreo correo, UsuarioTelefono telefono, UsuarioDireccion direccion)
        {
            this.Id = Id;
            Nombre = nombre;
            Apellido= apellido;
            Correo = correo;
            Telefono = telefono;
            Direccion = direccion;
        }

    }
}

