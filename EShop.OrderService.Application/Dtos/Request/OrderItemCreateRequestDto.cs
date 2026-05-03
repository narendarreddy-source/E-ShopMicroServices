using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EShop.OrderService.Application.Dtos.Request
{
    public class OrderItemCreateRequestDto
    {
        public Guid ProductId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than zero")]
        public int Quantity { get; set; }
        [Range(0.01, double.MaxValue, ErrorMessage = "TotalAmount must be greater than zero")]
        public decimal UnitPrice { get; set; }
    }
}
