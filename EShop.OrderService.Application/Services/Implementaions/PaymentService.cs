using AutoMapper;
using EShop.OrderService.Application.Dtos.Request;
using EShop.OrderService.Application.Dtos.Response;
using EShop.OrderService.Application.Repositories;
using EShop.OrderService.Application.Services.Interfaces;
using EShop.OrderService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.OrderService.Application.Services.Implementaions
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;
        private readonly IPaymentGateway _paymentGateway;
        public PaymentService(IPaymentRepository paymentRepository, IMapper mapper, IPaymentGateway paymentGateway)
        {
            _paymentRepository = paymentRepository;
            _mapper = mapper;
            _paymentGateway = paymentGateway;
        }
        public async Task<string> CreatePaymentIntentAsync(Guid orderid,decimal amount, CancellationToken cancellationToken)
        {
            var paymentIntent = await _paymentGateway.CreatePaymentIntentAsync(amount, orderid, cancellationToken);
            var payment = new Payment
            {
                OrderId = orderid,
                PaymentIntentId = paymentIntent.Id,
                PaymentStatus = paymentIntent.Status,
                PaymentMethod = paymentIntent.PaymentMethodTypes.FirstOrDefault() ?? "unknown",
                PaymentDate = DateTime.UtcNow
            };
            await _paymentRepository.AddPaymentAync(payment, cancellationToken);
            return paymentIntent.ClientSecret;
        }

        public async Task<GetPaymentDetailsResponseDto?> GetPaymentByOrderIdAsync(Guid orderId, CancellationToken cancellationToken)
        {
            var payment = await _paymentRepository.GetPaymentByOrderIdAsync(orderId, cancellationToken);
            return _mapper.Map<GetPaymentDetailsResponseDto>(payment);
        }

        public async Task<GetPaymentDetailsResponseDto?> GetPaymentByPaymentIntentIdAsync(string paymentIntentId, CancellationToken cancellationToken)
        {
            var payment = await _paymentRepository.GetPaymentByPaymentIntentIdAsync(paymentIntentId, cancellationToken);
            return _mapper.Map<GetPaymentDetailsResponseDto>(payment);
        }

        public async Task UpdatePaymentStatusAsync(string paymentIntentId, string newStatus, CancellationToken cancellationToken)
        {
            await _paymentRepository.UpdatePaymentStatusAync(paymentIntentId, newStatus, cancellationToken);
        }

      
    }
}
