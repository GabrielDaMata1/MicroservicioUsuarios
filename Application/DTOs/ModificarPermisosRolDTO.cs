namespace MicroservicioUsuarios.Application.DTOs
{
    public class ModificarPermisosRolDTO
    {
        public string NombreRol { get; set; }
        public List<string> NombresPermisos { get; set; }
    }
}
