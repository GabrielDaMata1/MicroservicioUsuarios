namespace MicroservicioUsuarios.Application.Exceptions
{
    public class AsignarRolInvalidoException: Exception
    {
        public AsignarRolInvalidoException():base("Errror, nombre de rol invalido") { }
    }
}
