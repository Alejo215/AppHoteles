﻿using AppHotel.Domain.ApplicationServiceContracts;
using AppHotel.ApplicationService.Services;
using AppHotel.Domain.RepositoryContracts;
using AppHotel.Infraestructure.Repository;
using AppHotel.Infraestructure.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using AppHotel.Domain.DTOs;
using AppHotel.Domain.Validators;

namespace AppHotel.Infraestructure.Extensions
{
    public static class ServiceExtennsions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            //Services
            services.AddScoped<IHotelService, HotelService>();
            services.AddScoped<IRoomService, RoomService>();

            //Repositories
            services.AddScoped<PersistenceContext>();
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            //Validators
            services.AddScoped<IValidator<HotelInDTO>, HotelInValidator>();
            services.AddScoped<IValidator<RoomInDTO>, RoomInValidator>();

            return services;
        }
    }
}
