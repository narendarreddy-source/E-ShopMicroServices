using EShop.CatalogService.Application.Dtos.Request;
using EShop.CatalogService.Application.Dtos.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.CatalogService.Application.Services.Interfaces
{
    public interface IProductService
    {
        public Task<IEnumerable<GetProductsDto>> GetAllAsync(CancellationToken cancellationToken);
        public Task<GetProductsDto> GetProductByIdAsync(Guid id, CancellationToken cancellationToken);
        public Task<GetProductsDto> AddProductAsync(AddProductDto product,CancellationToken cancellationToken);
        public Task<GetProductsDto> UpdateProductAsync(UpdateProductDto product, CancellationToken cancellationToken);
        public Task<bool> DeleteProductAsync(Guid id, CancellationToken cancellationToken);

    }
}
