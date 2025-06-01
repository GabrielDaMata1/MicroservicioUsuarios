using MicroserviciosUsuarios.Domain.Entities;
using MicroservicioUsuarios.Domain.Value_Object;

namespace MicroservicioUsuarios.Domain.Factory
{
    public static class UsuarioFactory
    {
        public static Usuario CrearUsuario(string nombre, string apellido, string correo, string contraseña, string telefono, string direccion)
        {
            var nombreVO = new UsuarioNombre(nombre);
            var apellidoVO = new UsuarioApellido(apellido);
            var correoVO = new UsuarioCorreo(correo);
            var contraseñaVO = new UsuarioContraseña(contraseña);
            var telefonoVO = new UsuarioTelefono(telefono);
            var direccionVO = new UsuarioDireccion(direccion);

            return new Usuario(nombreVO, apellidoVO, correoVO, contraseñaVO, telefonoVO, direccionVO);
        }

        public static Usuario CrearUsuarioConId(Guid id, string nombre,string apellido, string correo, string telefono, string direccion)
        {
            var nombreVO = new UsuarioNombre(nombre);
            var apellidoVO = new UsuarioApellido(apellido);
            var correoVO = new UsuarioCorreo(correo);
            var telefonoVO = new UsuarioTelefono(telefono);
            var direccionVO = new UsuarioDireccion(direccion);

            return new Usuario(id, nombreVO, apellidoVO, correoVO, telefonoVO, direccionVO);
        }
    }
}
