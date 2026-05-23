using Stripe;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.OrderService.Application.Repositories
{
    public interface IPaymentGateway
    {
        public Task<PaymentIntent> CreatePaymentIntentAsync(decimal amount, Guid orderId, CancellationToken cancellationToken);
    }
}
