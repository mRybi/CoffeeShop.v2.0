using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CoffeeShop.v2.Dto;
using CoffeeShop.v2.Models;

namespace CoffeeShop.v2.Helpers
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Coffee, CoffeeDto>();
            CreateMap<Cart, CartDto>();
            CreateMap<CartDto, Cart>();
            CreateMap<CoffeeDto, Coffee>();
            CreateMap<Cart, ItemsFromCartDto>();
            CreateMap<ItemsFromCartDto, Cart>();
            CreateMap<OrderToCreateDto, Order>();
            CreateMap<User, UserToRegister>();
            CreateMap<UserToRegister, User>();
            CreateMap<User,UserToReturnDto>();
            CreateMap<Order, OrderSummaryDto>();
        }
    }
}
