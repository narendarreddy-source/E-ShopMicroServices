using EShop.CatalogService.Application.Dtos.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.CatalogService.Application.Services.Interfaces
{
    public interface IProductService
    {
        public Task<IEnumerable<GetProductsDto>> GetAllAsync(CancellationToken cancellationToken);
    }
}
