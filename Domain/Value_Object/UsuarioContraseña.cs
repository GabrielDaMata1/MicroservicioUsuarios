namespace MicroservicioUsuarios.Domain.Value_Object
{
    public class UsuarioContraseña
    {
      public string contraseña { get; }

        public UsuarioContraseña(string contraseña)
        {
            this.contraseña = contraseña;
        }

    }
}
