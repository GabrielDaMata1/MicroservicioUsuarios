using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MicroservicioUsuarios.Infrastructure.Models
{
    public class UsuarioMongo
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; }

    public string Nombre { get; set; }

    public string Apellido { get; set; }
    public string Correo { get; set; }
    public string Telefono { get; set; }
    public string Direccion { get; set; }
    public int RolId { get; set; }
}
}