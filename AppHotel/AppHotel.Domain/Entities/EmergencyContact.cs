using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AppHotel.Domain.Entities
{
    public class EmergencyContact : BaseDocument
    {
        [BsonElement("bookingId")]
        public string? BookingId { get; set; }

        [BsonElement("name")]
        public string Name { get; set; } = null!;

        [BsonElement("phoneNumber")]
        public string PhoneNumber { get; set; } = null!;
    }
}
