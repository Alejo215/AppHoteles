using AppHotel.Domain.DTOs;
using AppHotel.Domain.Entities;

namespace AppHotel.Domain.ApplicationServiceContracts
{
    public interface IBookingService
    {
        Task<BookingOutDTO> CreateBooking(BookingInDTO bookingInDTO);

        Task<List<BookingOutDTO>> GetBooking(string? idHotel);

        Task<List<BookingDetailDTO>> GetDetail(string? idBooking);
    }
}
