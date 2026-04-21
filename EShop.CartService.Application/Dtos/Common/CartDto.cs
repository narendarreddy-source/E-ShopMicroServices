using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.CartService.Application.Dtos.Common
{
    public class CartDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public List<CartItemDto> CartItems { get; set; } = new List<CartItemDto>();
    }
}
