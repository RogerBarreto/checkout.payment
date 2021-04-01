using System.Net;
using Checkout.Gateway.Application.Authentication.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Checkout.WebApi.Common.Controllers;
using Checkout.WebApi.Common.Models;

namespace Checkout.Gateway.WebApi.Controllers
{
	[ApiController]
	[ApiVersion("1.0")]
	[Route("v{version:apiVersion}/[controller]")]
	public class AuthenticationController : BaseController
	{
		private readonly IMediator _mediator;

		public AuthenticationController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[Route("user")]
		[HttpPost]
		[ProducesResponseType(typeof(GetTokenResponse), (int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.BadRequest)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		public async Task<IActionResult> GetMerchantUserToken(GetMerchantUserTokenQuery query)
		{
			return (await _mediator.Send(query)).Match<IActionResult>(
				tokenResponse => Ok(tokenResponse),
				error => BadRequest(new ErrorModel(error.Message)));
		}

		[Route("api")]
		[HttpPost]
		[ProducesResponseType(typeof(GetTokenResponse), (int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.BadRequest)]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		public async Task<IActionResult> GetMerchantApiToken(GetMerchantApiTokenQuery query)
		{
			return (await _mediator.Send(query)).Match<IActionResult>(
				tokenResponse => Ok(tokenResponse), 
				error => BadRequest(new ErrorModel(error.Message)));
		}
	}
}
