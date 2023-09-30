using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AppHotel.Domain.Entities
{
    public class Room : BaseDocument
    {
        [BsonElement("hotelId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? HotelId { get; set; }

        [BsonElement("number")]
        public int Number { get; set; }

        [BsonElement("available")]
        public bool Available{ get; set; }

        [BsonElement("cost")]
        public float Cost { get; set; }

        [BsonElement("tax")]
        public float Tax { get; set; }

        [BsonElement("typeRoom")]
        public string TypeRoom { get; set; } = null!;

        [BsonElement("numberPeople")]
        public int NumberPeople { get; set; }

        [BsonIgnore]
        public Hotel? Hotel { get; set; }
    }
}
