using AutoMapper;
using EShop.CartService.Application.Dtos.Common;
using EShop.CartService.Application.Dtos.Request;
using EShop.CartService.Application.Dtos.Response;
using EShop.CartService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.CartService.Application.Mappers
{
    public class CartServiceMapper : Profile
    {
        public CartServiceMapper() {

            CreateMap<Cart,GetCartDto>().ReverseMap();
            CreateMap<Cart,UpdateCartDto>().ReverseMap();
            CreateMap<Cart,CartDto>().ReverseMap();
            CreateMap<CartItem, CartItemDto>().ReverseMap();
        }
    }
}
