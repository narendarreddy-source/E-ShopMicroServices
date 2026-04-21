using EShop.CartService.Application.Repositories;
using EShop.CartService.Domain.Entities;
using StackExchange.Redis;
using System.Text.Json;


namespace EShop.CartService.Infrastructure.Repositories
{
    public class RedisCartRepository : ICartRepository
    {
        private readonly IDatabase _db;
        private static readonly  string cartkey = "cart:";
        public RedisCartRepository(IConnectionMultiplexer connectionMultiplexer)
        {
            _db = connectionMultiplexer.GetDatabase();
        }
        public async Task<List<CartItem>?> GetAsync(string key, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var data = await _db
                .StringGetAsync($"{cartkey}{key}")
                .WaitAsync(cancellationToken);

            if (data.IsNullOrEmpty)
                return null;

            return JsonSerializer.Deserialize<List<CartItem>>(data.ToString());
        }

        public async Task SaveAsync(string key, List<CartItem> cart, TimeSpan? ttl = null, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var json = JsonSerializer.Serialize(cart);
            if (ttl.HasValue)
            {
                await _db
                    .StringSetAsync($"{cartkey}{key}", json, ttl.Value)
                    .WaitAsync(cancellationToken);
            }
            else
            {
                await _db
                    .StringSetAsync($"{cartkey}{key}", json)
                    .WaitAsync(cancellationToken);
            }

        }
        public async Task<bool> DeleteAsync(string key, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await _db
               .KeyDeleteAsync($"{cartkey}{key}")
               .WaitAsync(cancellationToken);
        }

    }
      
}
