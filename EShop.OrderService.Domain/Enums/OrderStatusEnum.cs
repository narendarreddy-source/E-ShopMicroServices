using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.OrderService.Domain.Enums
{
    public enum OrderStatusEnum
    {
        Pending = 1,
        Paid = 2,
        Shipped = 3,
        Delivered = 4,
        Cancelled = 5,
        Refunded = 6,
        Returned = 7
    }
}
