using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.CartService.Domain.Entities
{
    public class Cart
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public List<CartItem> CartItems { get; set; } = new List<CartItem>();
        public Guid? UserId { get; set; } 
    }
}
