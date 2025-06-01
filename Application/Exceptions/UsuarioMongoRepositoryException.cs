
    using System;
    namespace MicroservicioUsuarios.Application.Exceptions
    {
        public class UsuarioMongoRepositoryException : Exception
        {
            public UsuarioMongoRepositoryException() { }
            public UsuarioMongoRepositoryException(string message, Exception innerException) : base("message", innerException) { }
        }
    }
