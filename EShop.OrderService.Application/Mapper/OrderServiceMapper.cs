using AutoMapper;
using EShop.OrderService.Application.Dtos;
using EShop.OrderService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.OrderService.Application.Profiles
{
    public class OrderServiceMapper : Profile
    {
        public OrderServiceMapper() {

            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<OrderItem, OrderItemDto>().ReverseMap();
        }
    }
}
