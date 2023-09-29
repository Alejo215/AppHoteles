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
        public int Number { get; set; }

        [BsonElement("startDate")]
        public DateTime StartDate { get; set; }

        [BsonElement("endDate")]
        public DateTime EndDate { get; set; }
        public Room? Room { get; set; }
        public List<Guest>? ListGuest { get; set; }
        public EmergencyContact? EmergencyContact { get; set; }
    }
}
