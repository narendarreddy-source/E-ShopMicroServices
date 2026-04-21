using AutoMapper;
using EShop.CartService.Application.Dtos.Common;
using EShop.CartService.Application.Dtos.Request;
using EShop.CartService.Application.Dtos.Response;
using EShop.CartService.Application.Repositories;
using EShop.CartService.Application.Services.Interfaces;
using EShop.CartService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace EShop.CartService.Application.Services.Implementation
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;
        private static readonly TimeSpan ttl = TimeSpan.FromDays(7); // Set the TTL to 7 days

        public CartService(ICartRepository cartRepository, IMapper mapper)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
        }

        public async Task<bool> DeleteCartAsync(Guid key, CancellationToken cancellationToken)
        {
           return await _cartRepository.DeleteAsync(key.ToString(), cancellationToken);
        }

        public async Task<GetCartDto> GetOrCreateCartAsync(Guid key, CancellationToken cancellationToken)
        {
            var cart = await _cartRepository.GetAsync(key.ToString(), cancellationToken);
            if(cart == null)
            {
                var cartDto = new CartDto
                {
                    Id = key,
                    CartItems = new List<CartItemDto>()
                };

              var newCart = _mapper.Map<Cart>(cartDto);
               await _cartRepository.SaveAsync(key.ToString(), newCart.CartItems, ttl, cancellationToken);
                cart = await _cartRepository.GetAsync(key.ToString(), cancellationToken);
            }
            var returncart = new Cart
            {   
                Id = key,
                CartItems = cart
            };
            return _mapper.Map<GetCartDto>(returncart);
        }

        public async Task<GetCartDto> UpdateCartAsync(Guid key, List<CartItemDto> items, CancellationToken cancellationToken)
        {
             var oldcart = await _cartRepository.GetAsync(key.ToString(), cancellationToken);
            
            foreach (var item in items.Where(i => i.Quantity != 0)) { 
                var existingItem = oldcart.FirstOrDefault(i => i.ProductId == item.ProductId);
                if (existingItem != null)
                {
                    existingItem.Quantity += item.Quantity;
                    if (existingItem.Quantity <= 0)
                        oldcart.Remove(existingItem);
                }
                else
                {
                    oldcart.Add(_mapper.Map<CartItem>(item));
                }   
            }
            await _cartRepository.SaveAsync(key.ToString(), oldcart, ttl, cancellationToken);
            var returncart = new Cart
            {
                Id = key,
                CartItems = await _cartRepository.GetAsync(key.ToString(), cancellationToken)
            };
            return _mapper.Map<GetCartDto>(returncart);
        }

        public async Task<GetCartDto> MergeCartAsync(Guid cartid, Guid userid, CancellationToken cancellationToken)
        {
             var cart = await _cartRepository.GetAsync(cartid.ToString(), cancellationToken);
             await _cartRepository.SaveAsync(userid.ToString(), cart, ttl, cancellationToken);
             await _cartRepository.DeleteAsync(cartid.ToString(), cancellationToken);
            var returncart = new Cart
            {
                Id = cartid,
                CartItems = cart,
                UserId = userid
            };
             return _mapper.Map<GetCartDto>(returncart);
        }
    }
}
