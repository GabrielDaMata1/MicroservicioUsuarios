using MongoDB.Bson.Serialization.Attributes;

namespace MicroservicioUsuarios.Infrastructure.Models
{
    public class RolesPermisosMongo
    {
            [BsonId]
           public RolPermisosIdMongo Id { get; set; }
        
    }
}
