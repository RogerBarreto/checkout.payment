using Checkout.Payment.Command.Application.Interfaces;
using Checkout.Payment.Command.Application.Models;
using Checkout.Payment.Command.Seedwork.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Checkout.Payment.Command.Controllers
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

        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.Accepted, Type = typeof(CreatePaymentResponseModel))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> CreatePaymentAsync([FromHeader] int merchantId, CreatePaymentRequestModel requestModel)
        {
            var createPayment = await _paymentService.TryCreatePaymentAsync(merchantId, requestModel);

            return Result(HttpStatusCode.Accepted, createPayment.Result);
        }

        [Route("{paymentId}")]
        [HttpPut]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(UpdatePaymentResponseModel), Description = "Successful - Payment request was updated")]
        [SwaggerResponse((int)HttpStatusCode.NoContent, Description = "Successful - Payment request not found, nothing updated")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> UpdatePaymentAsync([FromRoute] Guid paymentId, UpdatePaymentRequestModel requestModel)
        {
            var createPayment = await _paymentService.TryUpdatePaymentAsync(paymentId, requestModel);

            if (createPayment.Result)
            {
                return Result(HttpStatusCode.OK, new UpdatePaymentResponseModel(paymentId));
            }
            else
			{
                return Result(HttpStatusCode.NoContent);
            }
        }
    }
}
