using EShop.CartService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.CartService.Application.Repositories
{
    public interface ICartRepository
    {
        Task<List<CartItem>?> GetAsync(string key, CancellationToken cancellationToken);
        Task SaveAsync(string key, List<CartItem> cart, TimeSpan? ttl = null, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(string key, CancellationToken cancellationToken );
    }
}
