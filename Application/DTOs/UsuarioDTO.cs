namespace MicroservicioUsuarios.Application.DTOs
{
    public class UsuarioDTO
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }
        public string Contraseña { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }

        public UsuarioDTO(string nombre, string apellido, string correo, string telefono, string direccion)
        {
            Nombre = nombre;
            Apellido = apellido;
            Correo = correo;
            Telefono = telefono;
            Direccion = direccion;
        }
    }
}
