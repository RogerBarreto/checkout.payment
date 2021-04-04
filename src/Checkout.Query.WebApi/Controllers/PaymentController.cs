using System;
using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Checkout.Application.Common.Models.Payments.Queries;
using Checkout.WebApi.Common.Controllers;

namespace Checkout.Query.WebApi.Controllers
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

		[HttpGet]
		[Route("{paymentId}")]
		[ProducesResponseType(typeof(GetPaymentQueryResponse), (int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		[ProducesResponseType((int)HttpStatusCode.BadRequest)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		public async Task<IActionResult> GetPaymentAsync([FromRoute] Guid paymentId, [FromHeader] int merchantId)
		{
			var query = new GetPaymentQuery(paymentId, merchantId);
			
			return (await _mediator.Send(query))
				.Match<IActionResult>(
					resultOk => Ok(resultOk),
					notFound => NotFound(notFound),
					error => BadRequest(error)
				);
		}
	}
}
