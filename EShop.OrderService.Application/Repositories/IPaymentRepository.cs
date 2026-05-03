using EShop.OrderService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.OrderService.Application.Repositories
{
    public interface IPaymentRepository
    {
        Task AddPaymentAync (Payment payment, CancellationToken cancellationToken);
        Task UpdatePaymentAync(Payment payment, CancellationToken cancellationToken);
        Task<Payment?> GetPaymentByPaymentIntentIdAsync(string paymentIntentId, CancellationToken cancellationToken);
        Task<Payment?> GetPaymentByOrderIdAsync(Guid orderId, CancellationToken cancellationToken);
    }
}
