﻿using AutoMapper;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.JSInterop;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;
using System.Security.Cryptography;

namespace RestaurantAPI
{
    public class RestaurantMappingProfile : Profile
    {
        public RestaurantMappingProfile()
        {
            CreateMap<Restaurant, RestaurantDto>()
                .ForMember(dest => dest.Street,
                opt => opt.MapFrom(src => src.Address.Street))
                .ForMember(dest => dest.City,
                opt => opt.MapFrom(src => src.Address.City))
                .ForMember(dest => dest.PostalCode,
                opt => opt.MapFrom(src => src.Address.PostalCode));

            CreateMap<Dish, DishDto>();
        }
    }
}