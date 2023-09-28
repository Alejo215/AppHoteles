namespace AppHotel.Domain.DTOs
{
    public class RoomInDTO
    {
        public string? HotelId { get; set; }
        public int Number { get; set; }
        public float Cost { get; set; }
        public float Tax { get; set; }
        public string TypeRoom { get; set; } = null!;
        public string Location { get; set; } = null!;
        public int NumberPeople { get; set; }
    }
}
