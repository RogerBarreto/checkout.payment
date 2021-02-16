using Checkout.Payment.Gateway.Application.Interfaces;
using Checkout.Payment.Gateway.Application.Models;
using Checkout.Payment.Gateway.Seedwork.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace Checkout.Payment.Gateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class PaymentController : BaseController
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IDomainNotificationBus notificationBus, IPaymentService paymentService) : base(notificationBus)
        {
            _paymentService = paymentService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreatePaymentResponseModel), (int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> CreatePaymentAsync(CreatePaymentRequestModel requestModel)
        {
            var responseModel = await _paymentService.CreatePaymentAsync(GetCurrentUserId().Value, requestModel);

            return Result(HttpStatusCode.Accepted, responseModel);
        }
    }
}
