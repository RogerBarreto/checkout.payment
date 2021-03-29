using Checkout.Gateway.Application.Authentication.Errors;
using MediatR;
using OneOf;

namespace Checkout.Gateway.Application.Authentication.Queries
{
	public class GetMerchantUserTokenQuery : IRequest<OneOf<GetTokenResponse, AuthenticationError>>
	{
		public string UserName { get; set; }
		public string Password { get; set; }

	}
}
