using AppHotel.Domain.DTOs;

namespace AppHotel.Domain.ApplicationServiceContracts
{
    public interface IRoomService
    {
        Task<RoomOutDTO> CreateRoom(RoomInDTO roomInDTO);

        Task<RoomOutDTO> UpdateRoom(string? id, RoomInUpdateDTO roomInUpdateDTO);
    }
}
