using AppHotel.Domain.DTOs;
using AppHotel.Domain.Entities;
using AutoMapper;

namespace AppHotel.ApplicationService.Profiles
{
    public class HotelProfile :Profile
    {
        public HotelProfile()
        {
            CreateMap<HotelInDTO, Hotel>()
                .ForMember(dest => dest.Available, origen => origen.MapFrom(map => true));
            CreateMap<Hotel, HotelOutDTO>();
        }
    }
}
