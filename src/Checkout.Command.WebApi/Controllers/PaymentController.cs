using System;
using System.Net;
using System.Threading.Tasks;
using Checkout.Application.Common.Payments.Commands;
using Checkout.Command.Application.Payments.Commands;
using Checkout.Command.WebApi.Models;
using Checkout.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Checkout.WebApi.Common.Controllers;
using Checkout.WebApi.Common.Models;
using MediatR;

namespace Checkout.Command.WebApi.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class PaymentController : BaseController
	{
		private readonly IMediator _mediator;

		public PaymentController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPost]
		[ProducesResponseType((int)HttpStatusCode.Accepted, Type = typeof(CreatePaymentCommandResponse))]
		[ProducesResponseType((int)HttpStatusCode.BadRequest)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		public async Task<IActionResult> CreatePaymentAsync([FromHeader] int merchantId, CreatePaymentRequest request)
		{
			var command = new CreatePaymentCommand
			{
				MerchantId = merchantId,
				Amount = request.Amount,
				CardNumber = request.CardNumber,
				CurrencyType = request.CurrencyType,
				CardExpiryMonth = request.CardExpiryMonth,
				CardExpiryYear = request.CardExpiryYear,
				CardCVV = request.CardCVV
			};
			
			return (await _mediator.Send(command)).Match<IActionResult>(
				response => Ok(response),
				error => BadRequest(new ErrorModel(error.Message))
			);
		}

		[Route("{paymentId}")]
		[HttpPut]
		[ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(UpdatePaymentCommandResponse))]
		[ProducesResponseType((int)HttpStatusCode.NoContent)]
		[ProducesResponseType((int)HttpStatusCode.BadRequest)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		public async Task<IActionResult> UpdatePaymentAsync([FromRoute] Guid paymentId, UpdatePaymentRequest request)
		{
			var command = new UpdatePaymentCommand()
			{
				PaymentId = paymentId,
				PaymentStatus = Enum.Parse<PaymentStatus>(request.PaymentStatus),
				BankPaymentId = request.BankPaymentId,
				PaymentStatusDescription = request.PaymentStatusDescription
			};
			
			return (await _mediator.Send(command)).Match<IActionResult>(
				response => Ok(response),
				notFound => NoContent(),
				error => BadRequest(new ErrorModel(error.Message))
			);
		}
    }
}
