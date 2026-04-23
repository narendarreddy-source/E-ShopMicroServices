using EShop.CartService.Application.Dtos.Common;
using EShop.CartService.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace EShop.CartService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private static readonly string CookieKey = "CartId";
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        private Guid GetCartIdfromCookie()
        {
            Guid cartId = Guid.Empty;

            // Try to read and parse the cookie
            if (!Request.Cookies.TryGetValue(CookieKey, out var cookieValue) ||
                !Guid.TryParse(cookieValue, out cartId) || cartId == Guid.Empty)
            {
                // Cookie missing or invalid → create new Guid
                cartId = Guid.NewGuid();

                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTimeOffset.UtcNow.AddDays(30)
                };

                Response.Cookies.Append(CookieKey, cartId.ToString(), cookieOptions);
            }
            return cartId;
        }

        [HttpGet("GetorCreateCart")]
        [ProducesResponseType(typeof(List<CartItemDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetOrCreateCart( CancellationToken cancellationToken)
        {
            // Use the Guid from cookie (or newly created)
            var cartId = GetCartIdfromCookie();
            var cart = await _cartService.GetOrCreateCartAsync(cartId, cancellationToken);

            return Ok(cart);
        }

        [HttpPost("UpdateCart")]
        [ProducesResponseType(typeof(List<CartItemDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateCart( [FromBody] List<CartItemDto> cartDto, CancellationToken cancellationToken)
        {
            var cart = await _cartService.UpdateCartAsync(GetCartIdfromCookie(), cartDto, cancellationToken);
            return Ok(cart);
        }

        [HttpDelete("ClearCart")]
        public async Task<IActionResult> ClearCart(CancellationToken cancellationToken)
        {
            var result = await _cartService.DeleteCartAsync(GetCartIdfromCookie(), cancellationToken);
            return NoContent();
        }
    }
}
