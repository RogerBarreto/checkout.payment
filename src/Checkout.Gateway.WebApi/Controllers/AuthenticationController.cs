using Checkout.Gateway.Application.Authentication.Queries;
using Checkout.WebApi.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Checkout.Gateway.WebApi.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class AuthenticationController : BaseController
	{
		private readonly IMediator _mediator;

		

		public AuthenticationController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[Route("/user")]
		[HttpGet]
		public async Task<IActionResult> GetMerchantUserToken(GetMerchantUserTokenQuery query)
		{
			return (await _mediator.Send(query)).Match<IActionResult>(
				tokenResponse => Ok(tokenResponse),
				error => BadRequest(error));
		}

		[Route("/api")]
		[HttpGet]
		public async Task<IActionResult> GetMerchantApiToken(GetMerchantApiTokenQuery query)
		{
			return (await _mediator.Send(query)).Match<IActionResult>(
				tokenResponse => Ok(tokenResponse), 
				error => BadRequest(error.Message));
		}
	}
}
