using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AppHotel.Domain.Entities
{
    public class Hotel : BaseDocument
    {
        [BsonElement("name")]
        public string Name { get; set; } = null!;

        [BsonElement("available")]
        public bool Available { get; set; }

        [BsonElement("location")]
        public string Location { get; set; } = null!;
    }
}
