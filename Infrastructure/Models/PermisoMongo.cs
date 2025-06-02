using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace MicroservicioUsuarios.Infrastructure.Models
{


    public class PermisoMongo
    {
        [BsonId]
        public int Id { get; set; } 


        [BsonElement("nombre")] 
        public string Nombre { get; set; } 
    }
}
