using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.CatalogService.Application.Dtos.Request
{
    public class UpdateProductDto : AddProductDto
    {
        public Guid Id { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
