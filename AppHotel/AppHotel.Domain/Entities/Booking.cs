using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AppHotel.Domain.Entities
{
    public class Booking : BaseDocument
    {
        [BsonElement("roomId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? RoomId { get; set; }

        [BsonElement("description")]
        public string Description { get; set; } = null!;

        [BsonElement("number")]
        public int NumberPeople { get; set; }

        [BsonElement("startDate")]
        public DateTime StartDate { get; set; }

        [BsonElement("endDate")]
        public DateTime EndDate { get; set; }

        [BsonIgnore]
        public Room? Room { get; set; }

        [BsonIgnore]
        public List<Guest>? ListGuest { get; set; }

        [BsonIgnore]
        public EmergencyContact? EmergencyContact { get; set; }
    }
}
