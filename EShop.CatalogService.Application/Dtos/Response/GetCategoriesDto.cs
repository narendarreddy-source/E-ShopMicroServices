using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.CatalogService.Application.Dtos.Response
{
    public class GetCategoriesDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
