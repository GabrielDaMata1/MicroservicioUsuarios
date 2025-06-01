using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class RolMongo
{
    [BsonId]
    public int Id { get; set; }

    [BsonElement("Nombre")]
    public string Nombre { get; set; }

    [BsonElement("Descripcion")]
    public string Descripcion { get; set; }
}