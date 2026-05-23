using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.OrderService.Infrastructure.ConfigSettings
{
    public class StripeSettings
    {
        public string SecretKey { get; set; }
        public string PublishableKey { get; set; }

        public string WebhookSecret { get; set; }
        public string Currency { get; set; }
    }
}
