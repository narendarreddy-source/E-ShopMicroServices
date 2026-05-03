using EShop.OrderService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.OrderService.Application.Repositories
{
    public interface IOrderRepository
    {
        // Define methods for order repository
        Task<Order> GetOrderByIdAsync(Guid orderId,CancellationToken cancellationToken);
        Task<IEnumerable<Order>> GetOrdersByUserIdAsync(Guid userId,CancellationToken cancellationToken);
        Task<Guid> AddOrderAsync(Order order,CancellationToken cancellationToken);
        Task UpdateOrderAsync(Order order,CancellationToken cancellationToken);
        Task<bool> DeleteOrderAsync(Guid orderId,CancellationToken cancellationToken);
        Task<IEnumerable<OrderStatus>> GetAllOrderStatusesAsync(CancellationToken cancellationToken);
    }
}
