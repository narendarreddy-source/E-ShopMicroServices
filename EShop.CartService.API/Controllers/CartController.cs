using EShop.CartService.Application.Dtos.Request;
using EShop.CartService.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
        // GET: api/<CartController>
        [HttpGet("GetCartByUserId/{userId}")]
        public async Task<IActionResult> GetCartByUserId(Guid userId, CancellationToken cancellationToken)
        {
            var cart = await _cartService.GetCartByUserIdAsync(userId, cancellationToken);
            if (cart == null)
            {
                return NotFound();
            }
            return Ok(cart);
        }
        [HttpGet("GetCartByCartId/{cartId}")]
        public async Task<IActionResult> GetCartByCartId(Guid cartId, CancellationToken cancellationToken)
        {
            var cart = await _cartService.GetCartByCartIdAsync(cartId, cancellationToken);
            if (cart == null)
            {
                return NotFound();
            }
            return Ok(cart);
        }

        [HttpPost("CreateCart")]
        public async Task<IActionResult> CreateCart([FromBody] AddCartDto addCartDto, CancellationToken cancellationToken)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (addCartDto == null)
                    return BadRequest("Cart data is null.");

                var createdCart = await _cartService.CreateCartAsync(addCartDto, cancellationToken);
                return Ok(createdCart);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest($"An error occurred while validating the model: {ex.Message}");
            }
            
        }

        [HttpPut("UpdateCart")]
        public async Task<IActionResult> UpdateCart([FromBody] UpdateCartDto updateCartDto, CancellationToken cancellationToken)
        {
            if (updateCartDto == null)
                return BadRequest("Cart data is null.");

            var updatedCart = await _cartService.UpdateCartAsync(updateCartDto, cancellationToken);
            if (updatedCart == null)
                return NotFound();

            return Ok(updatedCart);
        }

        [HttpDelete("DeleteCartByCartId/{cartid}")]
        public async Task<IActionResult> DeleteCartAsync(Guid cartid, CancellationToken cancellationToken)
        {
            var result = await _cartService.DeleteCartByCartIdAsync(cartid, cancellationToken);
            if (!result)
                return NotFound();
            return NoContent();
        }

        [HttpDelete("DeleteCartByUserId/{userId}")]
        public async Task<IActionResult> DeleteCartByUserIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            var result = await _cartService.DeleteCartByUserIdAsync(userId, cancellationToken);
            if (!result)
                return NotFound();
            return NoContent();
        }
    }
}
