using Checkout.Payment.Query.Application.Interfaces;
using Checkout.Payment.Query.Application.Models;
using Checkout.Payment.Query.Seedwork.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Checkout.Payment.Query.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    public class PaymentController : BaseController
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IDomainNotification bus, IPaymentService paymentService) : base(bus)
        {
            _paymentService = paymentService;
        }

        [Route("{paymentId}")]
        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(GetPaymentResponseModel))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> CreatePaymentAsync([FromHeader] int merchantId, [FromRoute] Guid paymentId)
        {
            var paymentDetailsResponse = await _paymentService.TryGetPaymentAsync(merchantId, paymentId);

            return Result(HttpStatusCode.OK, paymentDetailsResponse.Result);
        }
    }
}
