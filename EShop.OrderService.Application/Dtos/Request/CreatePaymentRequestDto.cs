using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.OrderService.Application.Dtos.Request
{
    public class CreatePaymentRequestDto
    {
        public Guid OrderId { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentStatus { get; set; }
        public string PaymentIntentId { get; set; }
    }
}
