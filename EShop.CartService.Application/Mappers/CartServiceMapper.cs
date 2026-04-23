using AutoMapper;
using EShop.CartService.Application.Dtos.Common;

using EShop.CartService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.CartService.Application.Mappers
{
    public class CartServiceMapper : Profile
    {
        public CartServiceMapper() {

            CreateMap<Cart,CartDto>().ReverseMap();
            CreateMap<CartItem,CartItemDto>().ReverseMap();
        }
    }
}
