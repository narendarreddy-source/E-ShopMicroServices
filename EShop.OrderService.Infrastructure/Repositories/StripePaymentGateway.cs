using EShop.OrderService.Application.Repositories;
using EShop.OrderService.Infrastructure.ConfigSettings;
using Microsoft.Extensions.Options;
using Stripe;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.OrderService.Infrastructure.Repositories
{
    public class StripePaymentGateway : IPaymentGateway
    {
        private readonly StripeSettings _stripeSettings;
        public StripePaymentGateway(IOptions<StripeSettings> stripeSettings)
        {
            _stripeSettings = stripeSettings.Value;
            StripeConfiguration.ApiKey = _stripeSettings.SecretKey;

        }
        public async Task<PaymentIntent> CreatePaymentIntentAsync(decimal amount, Guid orderId, CancellationToken cancellationToken)
        {

            var options = new PaymentIntentCreateOptions
            {
                Amount = (long)(amount * 100), // Convert to cents
                Currency = _stripeSettings.Currency,
                Metadata = new Dictionary<string, string>
                {
                    { "order_id", orderId.ToString() }
                }
            };

            var service = new PaymentIntentService();
            var paymentIntent = await service.CreateAsync(options, cancellationToken: cancellationToken);

            return paymentIntent;
        }
    }
}
