using EShop.CartService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.CartService.Application.Repositories
{
    public interface ICartRepository
    {
        public Task<Cart> CreateCartAsync(Cart cart, CancellationToken cancellationToken);
        public Task<Cart> UpdateCartAsync(Cart cart, CancellationToken cancellationToken);
        public Task<Cart?> GetCartByUserIdAsync(Guid userId,CancellationToken cancellationToken);
        public Task<Cart?> GetCartByCartIdAsync(Guid cartId, CancellationToken cancellationToken);
        public Task<bool> DeleteCartByCartIdAsync(Guid cartId, CancellationToken cancellationToken);
        public Task<bool> DeleteCartByUserIdAsync(Guid userId, CancellationToken cancellationToken);
    }
}
