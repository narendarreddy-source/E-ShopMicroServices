using EShop.OrderService.Application.Dtos.Request;
using EShop.OrderService.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EShop.OrderService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("CreatePaymentIntent/{Orderid}/{Amount}")]
        public async Task<IActionResult> CreatePaymentIntentAsync(Guid Orderid,decimal Amount, CancellationToken cancellationToken)
        {
            var payment = await _paymentService.CreatePaymentIntentAsync(Orderid, Amount, cancellationToken);
            return Ok(payment);
        }

        [HttpGet("GetPaymentByOrderId/{orderId}")]
        public async Task<IActionResult> GetPaymentByOrderIdAsync(Guid orderId, CancellationToken cancellationToken)
        {
            var paymentDetails = await _paymentService.GetPaymentByOrderIdAsync(orderId, cancellationToken);
            if (paymentDetails == null)
            {
                return NotFound();
            }
            return Ok(paymentDetails);
        }

        [HttpGet("GetPaymentByPaymentIntentId/{paymentIntentId}")]
        public async Task<IActionResult> GetPaymentByPaymentIntentIdAsync(string paymentIntentId, CancellationToken cancellationToken)
        {
            var paymentDetails = await _paymentService.GetPaymentByPaymentIntentIdAsync(paymentIntentId, cancellationToken);
            if (paymentDetails == null)
            {
                return NotFound();
            }
            return Ok(paymentDetails);
        }

        [HttpPut("UpdatePaymentStatus/{paymentIntentId}/{newStatus}")]
        public async Task<IActionResult> GetPaymentByOrderIdAsync(string paymentIntentId, string newStatus, CancellationToken cancellationToken)
        {
            await _paymentService.UpdatePaymentStatusAsync(paymentIntentId, newStatus, cancellationToken);
            return Ok();
        } 

    }
}
