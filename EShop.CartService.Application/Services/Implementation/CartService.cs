using AutoMapper;
using EShop.CartService.Application.Dtos.Request;
using EShop.CartService.Application.Dtos.Response;
using EShop.CartService.Application.Repositories;
using EShop.CartService.Application.Services.Interfaces;
using EShop.CartService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.CartService.Application.Services.Implementation
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;

        public CartService(ICartRepository cartRepository, IMapper mapper)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
        }
        public async Task<GetCartDto> CreateCartAsync(AddCartDto cart, CancellationToken cancellationToken)
        {
            var cartreqmapper = _mapper.Map<Cart>(cart);
            var createdCart = await _cartRepository.CreateCartAsync(cartreqmapper, cancellationToken);
            return _mapper.Map<GetCartDto>(createdCart);
        }

        public async Task<bool> DeleteCartByCartIdAsync(Guid cartId, CancellationToken cancellationToken)
        {
            return await _cartRepository.DeleteCartByCartIdAsync(cartId, cancellationToken);
        }

        public async Task<bool> DeleteCartByUserIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            return await _cartRepository.DeleteCartByUserIdAsync(userId, cancellationToken);
        }

        public async Task<GetCartDto?> GetCartByCartIdAsync(Guid cartId, CancellationToken cancellationToken)
        {
            var cart = await _cartRepository.GetCartByCartIdAsync(cartId, cancellationToken);
            return _mapper.Map<GetCartDto?>(cart);
        }

        public async Task<GetCartDto?> GetCartByUserIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            var cart = await _cartRepository.GetCartByUserIdAsync(userId, cancellationToken);
            return _mapper.Map<GetCartDto?>(cart);
        }

        public async Task<GetCartDto> UpdateCartAsync(UpdateCartDto cart, CancellationToken cancellationToken)
        {
            var cartreqmapper = _mapper.Map<Cart>(cart);
            var updatedCart = await _cartRepository.UpdateCartAsync(cartreqmapper, cancellationToken);
            return _mapper.Map<GetCartDto>(updatedCart);
        }
    }
}
