using MongoDB.Bson.Serialization.Attributes;

namespace AppHotel.Domain.DTOs
{
    public class BookingAvailableOutDTO
    {
        public string? RoomId { get; set; }
        public int Number { get; set; }
        public float Cost { get; set; }
        public float Tax { get; set; }
        public string TypeRoom { get; set; } = null!;
        public int NumberMaxPeople { get; set; }
        public string Location { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}