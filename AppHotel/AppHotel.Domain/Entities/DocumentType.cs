using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AppHotel.Domain.Entities
{
    public class DocumentType : BaseDocument
    {
        [BsonElement("name")]
        public string Name { get; set; } = null!;
    }
}
