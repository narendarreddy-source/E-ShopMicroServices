using EShop.OrderService.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.OrderService.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }
        public decimal TotalAmount { get; set; }
        public int OrderStatusId { get; set; }   = (int)OrderStatusEnum.Pending;   
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; } = null;

        // Navigation
        public List<OrderItem> Items { get; set; }
        public Payment Payment { get; set; }
        public OrderStatus OrderStatus { get; set; }

    }
}
