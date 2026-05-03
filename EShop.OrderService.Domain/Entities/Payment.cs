using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.OrderService.Domain.Entities
{
    public class Payment
    {
        public Guid Id { get; set; } 
        public Guid OrderId { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentStatus { get; set; }
        public string PaymentIntentId { get; set; }
        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;

        // Navigation
        public Order Order { get; set; }
    }
}
