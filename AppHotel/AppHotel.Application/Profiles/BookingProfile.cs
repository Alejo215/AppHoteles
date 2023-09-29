using AppHotel.Domain.DTOs;
using AppHotel.Domain.Entities;
using AutoMapper;

namespace AppHotel.ApplicationService.Profiles
{
    public class BookingProfile : Profile
    {
        public BookingProfile() 
        {
            CreateMap<BookingInDTO, Booking>();
            CreateMap<Booking, BookingOutDTO>()
                .ForMember(x => x.Room, opt => opt.MapFrom(src => src.Room));
            CreateMap<Booking, BookingDetailDTO>()
                .ForMember(x => x.ListGuest, opt => opt.MapFrom(src => src.ListGuest))
                .ForMember(x => x.EmergencyContact, opt => opt.MapFrom(src => src.EmergencyContact));
        }
    }
}
