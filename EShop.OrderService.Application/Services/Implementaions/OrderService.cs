using AutoMapper;
using EShop.OrderService.Application.Dtos;
using EShop.OrderService.Application.Repositories;
using EShop.OrderService.Application.Services.Interfaces;
using EShop.OrderService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.OrderService.Application.Services.Implementaions
{
    public class OrderService : IOrderService
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;

        public OrderService(IMapper mapper, IOrderRepository orderRepository)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
        }
        public async Task<Guid> CreateOrderAsync(Guid userId, List<OrderItemDto> orderItems, CancellationToken cancellationToken)
        {
            var orderItemsEntities = _mapper.Map<List<OrderItem>>(orderItems);
            var order = new Order
            {
                UserId = userId,
                Items = orderItemsEntities, 
            };
            return await _orderRepository.AddOrderAsync(order, cancellationToken);;
        }

        public async Task<OrderDto> GetOrderByIdAsync(Guid orderId, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId, cancellationToken);
            return _mapper.Map<OrderDto>(order);
        }

        public async Task<List<OrderDto>> GetOrdersByUserIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetOrdersByUserIdAsync(userId, cancellationToken);
            return _mapper.Map<List<OrderDto>>(orders);
        }

        public async Task UpdateOrderStatusAsync(Guid orderId, int newStatusId, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId, cancellationToken);
            if (order == null)
            {
                throw new KeyNotFoundException("Order not found");
            }
            order.OrderStatusId = newStatusId;
            order.UpdatedAt = DateTime.UtcNow;
            await _orderRepository.UpdateOrderAsync(order, cancellationToken);
        }
    }
}
