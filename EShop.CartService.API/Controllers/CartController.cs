using EShop.CartService.Application.Dtos.Common;
using EShop.CartService.Application.Dtos.Request;
using EShop.CartService.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace EShop.CartService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("GetorCreateCart/{userId}")]
        public async Task<IActionResult> GetOrCreateCart(Guid userId, CancellationToken cancellationToken)
        {
            var cart = await _cartService.GetOrCreateCartAsync(userId, cancellationToken);
            return Ok(cart);
        }

        [HttpPost("UpdateCart")]
        public async Task<IActionResult> UpdateCart( [FromBody] UpdateCartDto cartDto, CancellationToken cancellationToken)
        {
            var cart = await _cartService.UpdateCartAsync(cartDto.Id, cartDto.CartItems, cancellationToken);
            return Ok(cart);
        }

        [HttpDelete("DeleteCart/{userId}")]
        public async Task<IActionResult> DeleteCart(Guid userId, CancellationToken cancellationToken)
        {
            var result = await _cartService.DeleteCartAsync(userId, cancellationToken);
            return NoContent();
        }
    }
}
