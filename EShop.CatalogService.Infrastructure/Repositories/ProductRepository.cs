using EShop.CatalogService.Application.Repositories;
using EShop.CatalogService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.CatalogService.Infrastructure.Repositories
{
    public class ProductRepository : IProdcutRepository
    {
        private readonly CatalogDbContext _dbContext;
        public ProductRepository(CatalogDbContext catalogDb) { 
            _dbContext = catalogDb;
        }
        public async Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Products.ToListAsync(cancellationToken);
        }
    }
}
