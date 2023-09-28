using AppHotel.ApplicationService.Exceptions;
using AppHotel.Domain.ApplicationServiceContracts;
using AppHotel.Domain.DTOs;
using AppHotel.Domain.Entities;
using AppHotel.Domain.RepositoryContracts;
using AutoMapper;

namespace AppHotel.ApplicationService.Services
{
    public class RoomService : IRoomService
    {
        private readonly IBaseRepository<Room> _baseRepository;
        private readonly IHotelService _hotelService;
        private readonly IMapper _mapper;

        public RoomService(IBaseRepository<Room> baseRepository, IHotelService hotelService, IMapper mapper)
        {
            _baseRepository = baseRepository;
            _hotelService = hotelService;
            _mapper = mapper;
        }

        public async Task<RoomOutDTO> CreateRoom(RoomInDTO roomInDTO)
        {
            _ = await _hotelService.GetHotelById(roomInDTO.HotelId);

            Room room = _mapper.Map<Room>(roomInDTO);
            await _baseRepository.AddAsync(room);
            RoomOutDTO roomOutDTO = _mapper.Map<RoomOutDTO>(room);
            return roomOutDTO;
        }

        public async Task<RoomOutDTO> UpdateRoom(string? id, RoomInUpdateDTO roomInUpdateDTO)
        {
            Room? room = (await _baseRepository.GetByAsync(x => x.Id == id)).FirstOrDefault() ?? throw new NotFoundApplicationException("La habitación no existe");
            _ = await _hotelService.GetHotelById(roomInUpdateDTO.HotelId);

            Room roomUpdated = _mapper.Map<Room>(roomInUpdateDTO);
            await _baseRepository.UpdateAsync(roomUpdated, id);
            RoomOutDTO roomOutDTO = _mapper.Map<RoomOutDTO>(roomUpdated);
            return roomOutDTO;
        }

        public async Task<RoomOutDTO> UpdateAvailabilityRoom(string? id, bool Available)
        {
            Room? room = (await _baseRepository.GetByAsync(x => x.Id == id)).FirstOrDefault() ?? throw new NotFoundApplicationException("La habitación no existe");

            room.Available = Available;
            await _baseRepository.UpdateAsync(room, id);
            RoomOutDTO roomOutDTO = _mapper.Map<RoomOutDTO>(room);
            return roomOutDTO;
        }
    }
}
