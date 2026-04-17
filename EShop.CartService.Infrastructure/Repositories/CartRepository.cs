using EShop.CartService.Application.Repositories;
using EShop.CartService.Domain.Entities;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;

namespace EShop.CartService.Infrastructure.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly IDatabase _db;
        private readonly TimeSpan _ttl = TimeSpan.FromDays(7);
        private const string _cartKeyPrefix = "cart:";
        private const string _userCartKeyPrefix = "user_cart:";
        public CartRepository(IConnectionMultiplexer db)
        {
            _db = db.GetDatabase();
        }

        public async Task<Cart> CreateCartAsync(Cart cart, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var serializedCart = JsonSerializer.Serialize(cart);

            if (cart.UserId == Guid.Empty)
                await _db.StringSetAsync($"{_cartKeyPrefix}{cart.Id}", serializedCart, _ttl);
            else
             await _db.StringSetAsync($"{_userCartKeyPrefix}{cart.UserId}", serializedCart, _ttl);
            
            return cart;
        }

        public async Task<bool> DeleteCartByCartIdAsync(Guid cartId,CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var key = $"{_cartKeyPrefix}{cartId}";

            bool deleted = await _db
                .KeyDeleteAsync(key)
                .WaitAsync(cancellationToken);

            return deleted;
        }

        public async Task<bool> DeleteCartByUserIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var key = $"{_userCartKeyPrefix}{userId}";

            bool deleted = await _db
                .KeyDeleteAsync(key)
                .WaitAsync(cancellationToken);

            return deleted;
        }

        public async Task<Cart?> GetCartByCartIdAsync(Guid cartId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var redisValue = await _db
                .StringGetAsync($"{_cartKeyPrefix}{cartId}")
                .WaitAsync(cancellationToken);

            if (redisValue.IsNullOrEmpty)
                return null;

            var json = redisValue.ToString();

            return JsonSerializer.Deserialize<Cart>(json);
        }

        public async Task<Cart?> GetCartByUserIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var redisValue = await _db
                .StringGetAsync($"{_userCartKeyPrefix}{userId}")
                .WaitAsync(cancellationToken);

            if (redisValue.IsNullOrEmpty)
                return null;

            var json = redisValue.ToString();

            return JsonSerializer.Deserialize<Cart>(json);
        }

        public async Task<Cart> UpdateCartAsync(Cart cart, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var cartJson = JsonSerializer.Serialize(cart);

            if (cart.UserId == Guid.Empty)
            {
                // Anonymous cart
                await _db.StringSetAsync($"{_cartKeyPrefix}{cart.Id}", cartJson, _ttl);
            }
            else
            {
                var userKey = $"{_userCartKeyPrefix}{cart.UserId}";
                var anonymousKey = $"{_cartKeyPrefix}{cart.Id}";
                // Save user cart
                await _db.StringSetAsync(userKey, cartJson, _ttl);

                // Delete anonymous cart after migration
                await _db.KeyDeleteAsync(anonymousKey);
            }
            return cart;
        }
    }
}
