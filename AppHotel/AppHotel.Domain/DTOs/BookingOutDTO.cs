using AppHotel.Domain.Entities;

namespace AppHotel.Domain.DTOs
{
    public class BookingOutDTO
    {
        public string? Id { get; set; }
        public string? RoomId { get; set; }
        public string Description { get; set; } = null!;
        public int NumberPeople { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Room? Room { get; set; }
    }
}
