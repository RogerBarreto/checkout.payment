using Checkout.Payment.Command.Application.Interfaces;
using Checkout.Payment.Command.Application.Models;
using Checkout.Payment.Command.Seedwork.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace Checkout.Payment.Command.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentController : BaseController
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IDomainNotification bus, IPaymentService paymentService) : base(bus)
        {
            _paymentService = paymentService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreatePaymentResponseModel), (int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> CreatePaymentAsync([FromHeader] int merchantId, CreatePaymentRequestModel requestModel)
        {
            var createPayment = await _paymentService.TryCreatePaymentAsync(merchantId, requestModel);

            return Result(HttpStatusCode.Accepted, createPayment.Result);
        }
    }
}
