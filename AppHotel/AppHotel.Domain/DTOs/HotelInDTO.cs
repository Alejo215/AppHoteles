namespace AppHotel.Domain.DTOs
{
    public class HotelInDTO
    {
        public string? Id { get; set; }

        public string Name { get; set; } = null!;

        public bool Available { get; set; }

        public string Location { get; set; } = null!;
    }
}
