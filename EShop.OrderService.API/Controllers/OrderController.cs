using EShop.OrderService.Application.Dtos;
using EShop.OrderService.Application.Dtos.Request;
using EShop.OrderService.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
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
            var orders = await _orderService.GetOrdersByUserIdAsync(ReturnUserId(), cancellationToken);
            return Ok(orders);
        }

        [HttpPost("CreateOrderAsync")]
        public async Task<IActionResult> CreateOrderAsync([FromBody] List<OrderItemCreateRequestDto> request, CancellationToken cancellationToken)
        {
            var userid = Guid.NewGuid();
            var order = await _orderService.CreateOrderAsync(ReturnUserId(), request, cancellationToken);
            return Ok(order);
        }

        [HttpPut("UpdateOrderAsync")]
        public async Task<IActionResult> UpdateOrderAsync(Guid orderid,int status, CancellationToken cancellationToken)
        {
            await _orderService.UpdateOrderStatusAsync(orderid, status, cancellationToken);
            return Ok();
        }
        [HttpGet("GetAllOrderStatusesAsync")]
        public async Task<IActionResult> GetAllOrderStatusesAsync( CancellationToken cancellationToken)
        {
            var orderstatus = await _orderService.GetAllOrderStatusesAsync(cancellationToken);
            if (orderstatus == null)
            {
                return NotFound();
            }
            return Ok(orderstatus);
        }
        private Guid ReturnUserId()
        {
            return Guid.Parse("CB3B96A1-99ED-4F97-9317-D5990534238A");
        }

    }
}
