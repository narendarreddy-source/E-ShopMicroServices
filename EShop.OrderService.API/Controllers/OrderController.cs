using Microsoft.AspNetCore.Mvc;
using EShop.OrderService.Application.Services.Interfaces;
using EShop.OrderService.Application.Dtos;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EShop.OrderService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
       
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("GetOrderByIdAsync/{orderid}")]
        public async Task<IActionResult> GetOrderByIdAsync(Guid orderid,CancellationToken cancellationToken)
        {
            var orders = await _orderService.GetOrderByIdAsync(orderid,cancellationToken);
            return Ok(orders);
        }

        [HttpGet("GetOrdersByUserIdAsync/{userid}")]
        public async Task<IActionResult> GetOrdersByUserIdAsync(Guid userid, CancellationToken cancellationToken)
        {
            var orders = await _orderService.GetOrdersByUserIdAsync(userid, cancellationToken);
            return Ok(orders);
        }

        [HttpPost("CreateOrderAsync")]
        public async Task<IActionResult> CreateOrderAsync([FromBody] List<OrderItemDto> request,Guid UserId, CancellationToken cancellationToken)
        {
            var order = await _orderService.CreateOrderAsync(UserId, request, cancellationToken);
            return Ok(order);
        }

        [HttpPut("UpdateOrderAsync")]
        public async Task<IActionResult> UpdateOrderAsync(Guid orderid,int status, CancellationToken cancellationToken)
        {
            await _orderService.UpdateOrderStatusAsync(orderid, status, cancellationToken);
            return Ok();
        }
    }
}
