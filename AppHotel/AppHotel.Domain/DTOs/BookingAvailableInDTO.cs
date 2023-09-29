namespace AppHotel.Domain.DTOs
{
    public class BookingAvailableInDTO
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Location { get; set; } = null!;
        public int NumberPeople { get; set; }
    }
}