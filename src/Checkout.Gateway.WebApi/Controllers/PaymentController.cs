using System;
using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Checkout.Application.Common.Payments.Commands;
using Checkout.Application.Common.Payments.Queries;
using Checkout.WebApi.Common.Controllers;
using Checkout.WebApi.Common.Models;
using Microsoft.AspNetCore.Authorization;

namespace Checkout.Gateway.WebApi.Controllers
{

	[Authorize]
	[ApiController]
	[ApiVersion("1.0")]
	[Route("v{version:apiVersion}/[controller]")]
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
			return (await _mediator.Send(requestModel))
				.Match<IActionResult>(
					resultOk => Ok(resultOk),
					error => BadRequest(new ErrorModel(error.Message))
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
					notFound => NotFound(new ErrorModel(notFound.Message)),
					error => BadRequest(new ErrorModel(error.Message))
				);
		}
	}
}
