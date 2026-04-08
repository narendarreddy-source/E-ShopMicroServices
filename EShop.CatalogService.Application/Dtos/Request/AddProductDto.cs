using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.CatalogService.Application.Dtos.Request
{
    public class AddProductDto
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public decimal Price { get; set; } = default!;
        public string ImageUrl { get; set; } = default!;
        public Guid CategoryId { get; set; } = default!;
    }
}
