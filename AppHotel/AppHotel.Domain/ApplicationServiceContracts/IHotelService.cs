using AppHotel.Domain.DTOs;

namespace AppHotel.Domain.ApplicationServiceContracts
{
    public interface IHotelService
    {
        Task<HotelOutDTO> CreateHotel(HotelInDTO hotelInDTO);
    }
}
