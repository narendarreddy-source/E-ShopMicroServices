using EShop.CatalogService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.CatalogService.Application.Repositories
{
    public interface IProdcutRepository
    {
        Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken);
    }
}
