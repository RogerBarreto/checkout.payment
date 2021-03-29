using System;
using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Checkout.Gateway.Application.Payments.Commands;
using Checkout.Gateway.Application.Payments.Queries;
using Checkout.WebApi.Common.Controllers;

namespace Checkout.Gateway.WebApi.Controllers
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
		[ProducesResponseType(typeof(CreatePaymentCommandResponse), (int)HttpStatusCode.Accepted)]
		[ProducesResponseType((int)HttpStatusCode.BadRequest)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		public async Task<IActionResult> CreatePaymentAsync(CreatePaymentCommand requestModel)
		{
			return (await _mediator.Send(requestModel)).Match<IActionResult>(
					resultOk => Ok(resultOk),
					error => BadRequest(error)
				);
		}

		[HttpGet]
		[Route("{paymentId}")]
		[ProducesResponseType(typeof(GetPaymentQueryResponse), (int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		[ProducesResponseType((int)HttpStatusCode.BadRequest)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		public async Task<IActionResult> GetPaymentAsync([FromRoute] Guid paymentId)
		{
			var query = new GetPaymentQuery(paymentId, GetCurrentUserId());
			
			return (await _mediator.Send(query))
				.Match<IActionResult>(
					resultOk => Ok(resultOk),
					notFound => NotFound(notFound),
					error => BadRequest(error)
				);
		}
	}
}
