using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.CartService.Domain.Entities
{
    public class Cart
    {
        public Guid Id { get; set; }
        public List<CartItem> Items { get; set; } = new List<CartItem>();
        public Guid? UserId { get; set; } = Guid.Empty;
        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}
