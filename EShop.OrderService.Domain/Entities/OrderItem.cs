using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.OrderService.Domain.Entities
{
    public class OrderItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; } 
        public decimal TotalPrice => Quantity * UnitPrice; // Calculated property

        // Navigation
        public Order Order { get; set; }
    }
}
