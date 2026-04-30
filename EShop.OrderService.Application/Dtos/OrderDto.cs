using EShop.OrderService.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EShop.OrderService.Application.Dtos
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "TotalAmount must be greater than zero")]
        public decimal TotalAmount { get; set; }
        public int OrderStatusId { get; set; } 
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; } 

        public List<OrderItemDto> Items { get; set; } 
    }
}
