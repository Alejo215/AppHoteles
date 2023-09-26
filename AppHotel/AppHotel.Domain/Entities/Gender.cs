using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AppHotel.Domain.Entities
{
    public class Gender : BaseDocument
    {
        [BsonElement("name")]
        public string Name { get; set; } = null!;
    }
}
