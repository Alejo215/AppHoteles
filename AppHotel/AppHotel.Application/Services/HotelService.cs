using AppHotel.Domain.RepositoryContracts;
using AppHotel.Domain.Entities;
using AppHotel.Domain.ApplicationServiceContracts;
using AppHotel.Domain.DTOs;
using AutoMapper;
using AppHotel.ApplicationService.Exceptions;

namespace AppHotel.ApplicationService.Services
{
    public class HotelService : IHotelService
    {
        private readonly IBaseRepository<Hotel> _baseRepository;
        private readonly IRoomService _roomService;
        private readonly IMapper _mapper;

        public HotelService(IBaseRepository<Hotel> baseRepository, IRoomService roomService, IMapper mapper)
        {
            _baseRepository = baseRepository;
            _roomService = roomService;
            _mapper = mapper;
        }

        public async Task<HotelOutDTO> CreateHotel(HotelInDTO hotelInDTO)
        {
            Hotel hotel = _mapper.Map<Hotel>(hotelInDTO);
            await _baseRepository.AddAsync(hotel);
            HotelOutDTO hotelOutDTO = _mapper.Map<HotelOutDTO>(hotel);
            return hotelOutDTO;
        }

        public async Task<HotelOutDTO> GetHotelById(string? hotelId)
        {
            Hotel? hotel = (await _baseRepository.GetByAsync(x => x.Id == hotelId)).FirstOrDefault() ?? throw new NotFoundApplicationException("El hotel no existe");
            HotelOutDTO hotelOutDTO = _mapper.Map<HotelOutDTO>(hotel);
            return hotelOutDTO;
        }

        public async Task<HotelOutDTO> UpdateHotel(string? id, HotelInUpdateDTO hotelInUpdateDTO)
        {
            _ = await GetHotelById(id);

            Hotel hotelUpdated = _mapper.Map<Hotel>(hotelInUpdateDTO);
            await _baseRepository.UpdateAsync(hotelUpdated, id);
            HotelOutDTO hotelOutDTO = _mapper.Map<HotelOutDTO>(hotelUpdated);
            return hotelOutDTO;
        }

        public async Task<HotelOutDTO> UpdateAvailabilityHotel(string? id, bool Available)
        {
            Hotel? hotel = (await _baseRepository.GetByAsync(x => x.Id == id)).FirstOrDefault() ?? throw new NotFoundApplicationException("El hotel no existe");

            hotel.Available = Available;
            await _baseRepository.UpdateAsync(hotel, id);
            HotelOutDTO hotelOutDTO = _mapper.Map<HotelOutDTO>(hotel);
            return hotelOutDTO;
        }
    }
}
