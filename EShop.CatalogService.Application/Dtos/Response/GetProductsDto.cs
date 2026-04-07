using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.CatalogService.Application.Dtos.Response
{
    public class GetProductsDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public Guid CategoryId { get; set; }
    }
}
