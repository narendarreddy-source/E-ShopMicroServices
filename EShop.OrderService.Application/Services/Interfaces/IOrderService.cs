using EShop.OrderService.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.OrderService.Application.Services.Interfaces
{
    public interface IOrderService
    {
        Task<Guid> CreateOrderAsync(Guid userId, List<OrderItemDto> orderItems, CancellationToken cancellationToken);
        Task<OrderDto> GetOrderByIdAsync(Guid orderId, CancellationToken cancellationToken);
        Task<List<OrderDto>> GetOrdersByUserIdAsync(Guid userId, CancellationToken cancellationToken);
        Task UpdateOrderStatusAsync(Guid orderId, int newStatusId, CancellationToken cancellationToken);
    }
}
