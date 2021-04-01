using Checkout.Domain.Errors;
using MediatR;
using OneOf;

namespace Checkout.Gateway.Application.Authentication.Queries
{
	public class GetMerchantApiTokenQuery : IRequest<OneOf<GetTokenResponse, AuthenticationError>>
	{
		public string ApiSecret { get; set; }
		public string ApiKey { get; set; }

	}
}
