using EShop.CartService.Application.Dtos.Common;
using EShop.CartService.Application.Dtos.Request;
using EShop.CartService.Application.Dtos.Response;
using EShop.CartService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.CartService.Application.Services.Interfaces
{
    public interface ICartService
    {
        //public Task<GetCartDto> CreateCartAsync(AddCartDto cart, CancellationToken cancellationToken);
        //public Task<GetCartDto> UpdateCartAsync(UpdateCartDto cart, CancellationToken cancellationToken);
        //public Task<GetCartDto?> GetCartByUserIdAsync(Guid userId, CancellationToken cancellationToken);
        //public Task<GetCartDto?> GetCartByCartIdAsync(Guid cartId, CancellationToken cancellationToken);
        //public Task<bool> DeleteCartByCartIdAsync(Guid cartId, CancellationToken cancellationToken);
        //public Task<bool> DeleteCartByUserIdAsync(Guid userId, CancellationToken cancellationToken);

        Task<GetCartDto> GetOrCreateCartAsync(Guid key,CancellationToken cancellationToken);
        Task<GetCartDto> UpdateCartAsync(Guid key, List<CartItemDto> items, CancellationToken cancellationToken);
        Task<bool> DeleteCartAsync(Guid key, CancellationToken cancellationToken);

        Task<GetCartDto> MergeCartAsync(Guid cartid,Guid userid, CancellationToken cancellationToken);
    }
}
