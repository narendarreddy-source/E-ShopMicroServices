using EShop.OrderService.Application.Repositories;
using EShop.OrderService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.OrderService.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderDbContext _dbContext;
        public OrderRepository(OrderDbContext dbContext) {
            _dbContext = dbContext;
        }
        public async Task<Guid> AddOrderAsync(Order order, CancellationToken cancellationToken)
        {
            await _dbContext.Orders.AddAsync(order, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return order.Id;
        }

        public async Task<bool> DeleteOrderAsync(Guid orderId, CancellationToken cancellationToken)
        {
            var order = await _dbContext.Orders
                .Include(o => o.Items)
                .Include(o => o.Payment)
                .FirstOrDefaultAsync(o => o.Id == orderId, cancellationToken);

            if (order == null)
                return false;

            _dbContext.Orders.Remove(order);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<IEnumerable<OrderStatus>> GetAllOrderStatusesAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.OrderStatuses.ToListAsync(cancellationToken);
        }

        public async Task<Order> GetOrderByIdAsync(Guid orderId, CancellationToken cancellationToken)
        {
           var order = await _dbContext.Orders.Include(o => o.Items).Include(o => o.Payment)
                .FirstOrDefaultAsync(o => o.Id == orderId, cancellationToken);
            if (order == null)
            {
                throw new KeyNotFoundException($"Order with ID {orderId} not found.");
            }
            return order;
        }

        public Task<IEnumerable<Order>> GetOrdersByUserIdAsync(Guid userId, CancellationToken cancellationToken)
        {
           var orders = _dbContext.Orders.Include(o => o.Items).Include(o => o.Payment)
                .Where(o => o.UserId == userId);
            return Task.FromResult<IEnumerable<Order>>(orders);
        }

        public Task UpdateOrderAsync(Order order, CancellationToken cancellationToken)
        {
           var existingOrder = _dbContext.Orders.FirstOrDefault(o => o.Id == order.Id);
            if (existingOrder != null)
            {
                _dbContext.Entry(existingOrder).CurrentValues.SetValues(order);
                return _dbContext.SaveChangesAsync(cancellationToken);
            }
            else
            {
                throw new KeyNotFoundException($"Order with ID {order.Id} not found.");
            }

        }
    }
}
