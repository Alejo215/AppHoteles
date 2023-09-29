using AppHotel.Domain.Entities;

namespace AppHotel.Domain.DTOs
{
    public class BookingDetailDTO
    {
        public string? Id { get; set; }
        public string? RoomId { get; set; }
        public string Description { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<Guest>? ListGuest { get; set; }
        public EmergencyContact? EmergencyContact { get; set; }
    }
}