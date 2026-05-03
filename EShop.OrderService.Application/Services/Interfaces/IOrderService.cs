using EShop.OrderService.Application.Dtos;
using EShop.OrderService.Application.Dtos.Request;
using EShop.OrderService.Application.Dtos.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.OrderService.Application.Services.Interfaces
{
    public interface IOrderService
    {
        Task<Guid> CreateOrderAsync(Guid userId, List<OrderItemCreateRequestDto> orderItems, CancellationToken cancellationToken);
        Task<OrderDto> GetOrderByIdAsync(Guid orderId, CancellationToken cancellationToken);
        Task<List<OrderDto>> GetOrdersByUserIdAsync(Guid userId, CancellationToken cancellationToken);
        Task UpdateOrderStatusAsync(Guid orderId, int newStatusId, CancellationToken cancellationToken);
        Task<List<OrderStatusResponseDto>> GetAllOrderStatusesAsync(CancellationToken cancellationToken);
    }
}
