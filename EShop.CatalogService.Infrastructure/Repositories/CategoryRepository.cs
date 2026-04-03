using EShop.CatalogService.Application.Repositories;
using EShop.CatalogService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.CatalogService.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly CatalogDbContext _dbContext;
        public CategoryRepository(CatalogDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Category>> GetAllAsync(CancellationToken cancellationToken)
        {
           return await _dbContext.Categories.AsNoTracking().ToListAsync(cancellationToken);
        }
    }
}
