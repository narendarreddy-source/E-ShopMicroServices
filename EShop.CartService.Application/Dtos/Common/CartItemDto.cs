using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.CartService.Application.Dtos.Common
{
    public class CartItemDto
    {
        public Guid ProductId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
