using EShop.OrderService.Application.Repositories;
using EShop.OrderService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.OrderService.Infrastructure.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly OrderDbContext _dbContext;

        public PaymentRepository(OrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddPaymentAync(Payment payment, CancellationToken cancellationToken)
        {
            _dbContext.Payments.Add(payment);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Payment?> GetPaymentByOrderIdAsync(Guid orderId, CancellationToken cancellationToken)
        {
            return await _dbContext.Payments.FirstOrDefaultAsync(p => p.OrderId == orderId, cancellationToken);
        }

        public async Task<Payment?> GetPaymentByPaymentIntentIdAsync(string paymentIntentId, CancellationToken cancellationToken)
        {
            return await _dbContext.Payments.FirstOrDefaultAsync(p => p.PaymentIntentId == paymentIntentId, cancellationToken);
        }

        public async Task UpdatePaymentAync(Payment payment, CancellationToken cancellationToken)
        {
            _dbContext.Payments.Update(payment);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
