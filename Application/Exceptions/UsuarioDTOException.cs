namespace MicroservicioUsuarios.Application.Exceptions
{
    using System;
    using System.Collections.Generic;

    public class UsuarioDTOException : Exception
    {
        public UsuarioDTOException()
        : base("Se encontraron errores en la validación del DTO.")
        {
        }
    }
}