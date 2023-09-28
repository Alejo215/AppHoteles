using AppHotel.Domain.DTOs;
using AppHotel.Domain.Entities;
using AutoMapper;

namespace AppHotel.ApplicationService.Profiles
{
    public class RoomProfile : Profile
    {
        public RoomProfile()
        {
            CreateMap<RoomInDTO, Room>()
                .ForMember(dest => dest.Available, origen => origen.MapFrom(map => true));
            CreateMap<Room, RoomOutDTO>();
        }
    }
}
