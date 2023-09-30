using AppHotel.Domain.DTOs;
using AppHotel.Domain.Entities;
using AutoMapper;

namespace AppHotel.ApplicationService.Profiles
{
    public class GuestProfile : Profile
    {
        public GuestProfile()
        {
            CreateMap<GuestInDTO, Guest>();
        }
    }
}
