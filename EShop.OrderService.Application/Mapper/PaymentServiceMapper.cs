using AutoMapper;
using EShop.OrderService.Application.Dtos.Request;
using EShop.OrderService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.OrderService.Application.Mapper
{
    public class PaymentServiceMapper : Profile
    {
        public PaymentServiceMapper() { 

            CreateMap<Payment,GetPaymentDetailsResponseDto>();
            CreateMap<CreatePaymentRequestDto, Payment>();
        }
    }
}
