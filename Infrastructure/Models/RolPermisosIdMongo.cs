using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MicroservicioUsuarios.Infrastructure.Models
{
    public class RolPermisosIdMongo
    {
        [BsonElement("rolId")]
        public int RolId { get; set; }

        [BsonElement("permisoId")]
        public int PermisoId { get; set; }
    }
}
