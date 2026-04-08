using EShop.CatalogService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.CatalogService.Application.Repositories
{
    public interface IProdcutRepository
    {
        Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken);
        Task<Product> GetProductByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<Product> AddProductAsync(Product product, CancellationToken cancellationToken);
        Task<Product> UpdateProductAsync(Product product, CancellationToken cancellationToken);
        Task<bool> DeleteProductAsync(Guid id, CancellationToken cancellationToken);
    }
}
