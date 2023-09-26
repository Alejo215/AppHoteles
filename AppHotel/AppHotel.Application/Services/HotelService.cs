using AppHotel.Domain.RepositoryContracts;
using AppHotel.Domain.Entities;
using AppHotel.Domain.ApplicationServiceContracts;
using AppHotel.Domain.DTOs;
using AutoMapper;

namespace AppHotel.Application.Services
{
    public class HotelService : IHotelService
    {
        private readonly IBaseRepository<Hotel> _baseRepository;
        private readonly IMapper _mapper;

        public HotelService(IBaseRepository<Hotel> baseRepository, IMapper mapper)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
        }

        public async Task<HotelOutDTO> CreateHotel(HotelInDTO hotelInDTO)
        {
            Hotel hotel = _mapper.Map<Hotel>(hotelInDTO);
            await _baseRepository.AddAsync(hotel);
            HotelOutDTO hotelOutDTO = _mapper.Map<HotelOutDTO>(hotel);
            return hotelOutDTO;
        }
    }
}
