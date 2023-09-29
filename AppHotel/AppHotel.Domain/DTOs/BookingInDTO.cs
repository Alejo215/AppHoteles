namespace AppHotel.Domain.DTOs
{
    public class BookingInDTO
    {
        public string? RoomId { get; set; }
        public string Description { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
