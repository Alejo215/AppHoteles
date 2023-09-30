using AppHotel.Domain.DTOs;
using AppHotel.Domain.Entities;
using AutoMapper;

namespace AppHotel.ApplicationService.Profiles
{
    public class EmergencyContactProfile : Profile
    {
        public EmergencyContactProfile()
        {
            CreateMap<EmergencyContactInDTO, EmergencyContact>();
        }
    }
}
