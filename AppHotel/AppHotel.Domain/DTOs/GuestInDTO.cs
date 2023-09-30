namespace AppHotel.Domain.DTOs
{
    public class GuestInDTO
    {
        public string Name { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string? DocumentType { get; set; }
        public string Document { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
    }
}
