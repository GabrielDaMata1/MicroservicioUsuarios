using MongoDB.Bson.Serialization.Attributes;

public class RolConPermisosDTO
{
    [BsonId]
    public string RolNombre { get; set; }
    public List<string> permisos { get; set; }


}