using Eshop.Shared.Exceptions;
using EShop.CatalogService.Application.Repositories;
using EShop.CatalogService.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace EShop.CatalogService.Infrastructure.Repositories
{
    public class ProductRepository : IProdcutRepository
    {
        private readonly CatalogDbContext _dbContext;

        public ProductRepository(CatalogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Product> AddProductAsync(Product product, CancellationToken cancellationToken)
        {
            var entry = await _dbContext.Products.AddAsync(product, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return entry.Entity;
        }

        public async Task<bool> DeleteProductAsync(Guid id, CancellationToken cancellationToken)
        {
            var deletedCount = await _dbContext.Products
         .Where(p => p.Id == id)
         .ExecuteDeleteAsync(cancellationToken);

            return deletedCount > 0;
        }

        public async Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Products
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<Product> GetProductByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var product = await _dbContext.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

            if (product == null)
                throw new NotFoundException($"Product with id {id} not found");

            return product;
        }

        public async Task<Product> UpdateProductAsync(Product product, CancellationToken cancellationToken)
        {
            var updatedCount = await _dbContext.Products
                .Where(p => p.Id == product.Id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(p => p.Name, product.Name)
                    .SetProperty(p => p.Description, product.Description)
                    .SetProperty(p => p.Price, product.Price)
                    .SetProperty(p => p.ImageUrl, product.ImageUrl)
                    .SetProperty(p => p.CategoryId, product.CategoryId)
                    .SetProperty(p => p.UpdatedDate, DateTime.UtcNow),
                    cancellationToken);

            if (updatedCount == 0)
                throw new($"Product with id {product.Id} not found");

            // Reload the updated entity

            return await _dbContext.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == product.Id, cancellationToken)
                ?? throw new($"Product with id {product.Id} not found after update");
        }


    }
}
