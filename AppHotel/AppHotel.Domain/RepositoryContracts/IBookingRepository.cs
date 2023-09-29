using AppHotel.Domain.Entities;

namespace AppHotel.Domain.RepositoryContracts
{
    public interface IBookingRepository : IBaseRepository<Booking>
    {
        Task<List<Booking>> GetBookingByIdHotel(string? idHotel);

        Task<List<Booking>> EmergencyContact(string? idBooking);
    }
}
