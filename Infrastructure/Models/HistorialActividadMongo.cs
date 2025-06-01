using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
namespace MicroservicioUsuarios.Infrastructure.Models
{
    public class HistorialActividadMongo
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; }
    [BsonRepresentation(BsonType.String)]
    public Guid UsuarioId { get; set; }
    public string TipoAccion { get; set; }
    public DateTime FechaHora { get; set; }
}
}