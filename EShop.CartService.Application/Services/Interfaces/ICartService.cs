using EShop.CartService.Application.Dtos.Common;
using EShop.CartService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.CartService.Application.Services.Interfaces
{
    public interface ICartService
    {


        Task<List<CartItemDto>> GetOrCreateCartAsync(Guid key,CancellationToken cancellationToken);
        Task<List<CartItemDto>> UpdateCartAsync(Guid key, List<CartItemDto> items, CancellationToken cancellationToken);
        Task<bool> DeleteCartAsync(Guid key, CancellationToken cancellationToken);
        Task<bool> MergeCartAsync(Guid cartid,Guid userid, CancellationToken cancellationToken);
    }
}
