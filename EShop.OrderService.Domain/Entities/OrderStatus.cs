using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.OrderService.Domain.Entities
{
    public class OrderStatus
    {
        public int OrderStatusId { get; set; }
        public string Name { get; set; }

        // Navigation
        public List<Order> Orders { get; set; }
    }
}
