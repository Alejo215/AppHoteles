using AppHotel.Domain.DTOs;
using AppHotel.Domain.Entities;

namespace AppHotel.Domain.RepositoryContracts
{
    public interface IBookingRepository : IBaseRepository<Booking>
    {
        Task<List<Booking>> GetBookingByIdHotel(string? idHotel);

        Task<List<Booking>> GetDetail(string? idBooking);

        Task<List<BookingAvailableOutDTO>> GetAvailableBookings(BookingAvailableInDTO bookingAvailableInDTO);
    }
}
