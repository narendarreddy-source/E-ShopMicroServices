using EShop.OrderService.Application.Dtos.Request;
using EShop.OrderService.Application.Dtos.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EShop.OrderService.Application.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<string> CreatePaymentIntentAsync(Guid orderid,decimal amount, CancellationToken cancellationToken);
        Task<GetPaymentDetailsResponseDto?> GetPaymentByOrderIdAsync(Guid orderId, CancellationToken cancellationToken);
        Task<GetPaymentDetailsResponseDto?> GetPaymentByPaymentIntentIdAsync(string paymentIntentId, CancellationToken cancellationToken);
        Task UpdatePaymentStatusAsync(string paymentIntentId, string newStatus, CancellationToken cancellationToken);
    }
}
