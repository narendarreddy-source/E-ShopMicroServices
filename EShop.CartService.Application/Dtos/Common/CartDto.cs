using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.CartService.Application.Dtos.Common
{
    public class CartDto
    {
        public Guid Id { get; set; }
        public List<CartItemDto> Items { get; set; } = new List<CartItemDto>();
        public Guid? UserId { get; set; } = Guid.Empty;
 
    }
}
