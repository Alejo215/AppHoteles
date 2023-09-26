using AppHotel.Domain.RepositoryContracts;
using AppHotel.Domain.Entities;
using AppHotel.Domain.ApplicationServiceContracts;
using AppHotel.Domain.DTOs;

namespace AppHotel.Application.Services
{
    public class HotelService : IHotelService
    {
        private readonly IBaseRepository<Hotel> _baseRepository;

        public HotelService(IBaseRepository<Hotel> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<HotelOutDTO> CreateHotel(HotelInDTO hotelInDTO)
        {
            Hotel hotel = new()
            {
                Location = hotelInDTO.Location,
                Name = hotelInDTO.Name,
                Available = hotelInDTO.Available
            };
            await _baseRepository.AddAsync(hotel);
            HotelOutDTO hotelOutDTO = new()
            {
                Id = hotel.Id,
                Location = hotel.Location,
                Name = hotel.Name,
                Available = hotel.Available
            };
            return hotelOutDTO;
        }
    }
}
