using System;

namespace MicroservicioUsuarios.Application.Exceptions
{
    public class PermisoYaRegistradoException : Exception
    {
        public PermisoYaRegistradoException() : base("El permiso ya se encuentra registrado.")
        {
        }

        public PermisoYaRegistradoException(string message) : base(message)
        {
        }

        public PermisoYaRegistradoException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}