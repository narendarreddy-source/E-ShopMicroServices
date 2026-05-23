using EShop.OrderService.Application.Services.Implementaions;
using EShop.OrderService.Application.Services.Interfaces;
using EShop.OrderService.Infrastructure.ConfigSettings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;



namespace EShop.OrderService.API.Controllers
{
    [Route("api/webhooks/stripe")]
    [ApiController]
    public class StripeWebhookController : ControllerBase
    {
        private readonly StripeSettings _settings;
        private readonly IPaymentService _paymentService;
        public StripeWebhookController(IOptions<StripeSettings> options, IPaymentService paymentService)
        {
            _settings = options.Value;
            _paymentService = paymentService;
        }
        [HttpPost]
        public async Task<IActionResult> Handle()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            try
            {
                var stripeEvent = EventUtility.ConstructEvent(
                    json,
                    Request.Headers["Stripe-Signature"],
                    _settings.WebhookSecret
                );

                switch (stripeEvent.Type)
                {
                    
                    case EventTypes.PaymentIntentSucceeded:
                        var succeededIntent = stripeEvent.Data.Object as PaymentIntent;
                        // update DB
                       await  _paymentService.UpdatePaymentStatusAsync(succeededIntent.Id, succeededIntent.Status, CancellationToken.None);
                        break;

                    case EventTypes.PaymentIntentPaymentFailed:
                        var failedIntent = stripeEvent.Data.Object as PaymentIntent;
                        await _paymentService.UpdatePaymentStatusAsync(failedIntent.Id, failedIntent.Status, CancellationToken.None);
                        // update DB
                        break;

                    case EventTypes.PaymentIntentProcessing:
                        var processingIntent = stripeEvent.Data.Object as PaymentIntent;
                        await _paymentService.UpdatePaymentStatusAsync(processingIntent.Id, processingIntent.Status, CancellationToken.None);
                        // update DB
                        break;

                    case EventTypes.PaymentIntentCanceled:
                        var canceledIntent = stripeEvent.Data.Object as PaymentIntent;
                        await _paymentService.UpdatePaymentStatusAsync(canceledIntent.Id, canceledIntent.Status, CancellationToken.None);
 
                        // update DB
                        break;
                }

                return Ok();
            }
            catch (StripeException)
            {
                return BadRequest();
            }
        }

    }
}
