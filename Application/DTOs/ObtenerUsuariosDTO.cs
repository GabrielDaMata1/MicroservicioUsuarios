namespace MicroservicioUsuarios.Application.DTOs
{
    public class ObtenerUsuariosDTO
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }

        public string nombre_rol { get; set; }

        public ObtenerUsuariosDTO(string nombre, string apellido, string correo, string telefono, string direccion, string nombre_rol)
        {
            Nombre = nombre;
            Apellido = apellido;
            Correo = correo;
            Telefono = telefono;
            Direccion = direccion;
            this.nombre_rol = nombre_rol;
        }
    }
}
