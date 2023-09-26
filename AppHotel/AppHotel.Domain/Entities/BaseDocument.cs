using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace AppHotel.Domain.Entities
{
    public class BaseDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
    }
}
