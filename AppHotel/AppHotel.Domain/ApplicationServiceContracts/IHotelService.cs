﻿using AppHotel.Domain.DTOs;

namespace AppHotel.Domain.ApplicationServiceContracts
{
    public interface IHotelService
    {
        Task<HotelOutDTO> CreateHotel(HotelInDTO hotelInDTO);

        Task<HotelOutDTO> GetHotelById(string? hotelId);

        Task<HotelOutDTO> DeleteHotel(string? hotelId);
    }
}
