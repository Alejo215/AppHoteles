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
            Hotel? hotel = (await _baseRepository.GetByAsync(x => x.Id == hotelId)).FirstOrDefault();
            if (hotel == null)
                throw new NotFoundApplicationException("El hotel no existe");

            HotelOutDTO hotelOutDTO = _mapper.Map<HotelOutDTO>(hotel);
            return hotelOutDTO;
        }

        public async Task<HotelOutDTO> DeleteHotel(string? hotelId)
        {
            _ = await GetHotelById(hotelId);

            _ = await _roomService.DeleteManyRooms(hotelId);

            Hotel hotel = await _baseRepository.DeleteByAsync(hotelId);
            if (hotel == null)
                throw new NotFoundApplicationException("El hotel no existe");

            HotelOutDTO hotelOutDTO = _mapper.Map<HotelOutDTO>(hotel);
            return hotelOutDTO;
        }
    }
}
