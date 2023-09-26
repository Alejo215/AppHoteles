using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AppHotel.Domain.Entities
{
    public class Booking : BaseDocument
    {
        [BsonElement("roomId")]
        public string? RoomId { get; set; }

        [BsonElement("description")]
        public string Description { get; set; } = null!;

        [BsonElement("startDate")]
        public DateTime StartDate { get; set; }

        [BsonElement("endDate")]
        public DateTime EndDate { get; set; }
    }
}
