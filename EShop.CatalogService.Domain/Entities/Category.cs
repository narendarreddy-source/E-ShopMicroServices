using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.CatalogService.Domain.Entities
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; } 
        public ICollection<Product> Products { get; set; }
    }
}
