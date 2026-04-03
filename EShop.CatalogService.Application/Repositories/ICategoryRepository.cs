using EShop.CatalogService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.CatalogService.Application.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllAsync(CancellationToken cancellationToken);
    }
}
