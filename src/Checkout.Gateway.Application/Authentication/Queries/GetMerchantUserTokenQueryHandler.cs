using MediatR;
using OneOf;
using System.Threading;
using System.Threading.Tasks;
using Checkout.Application.Common.Models.Authentication;
using Checkout.Gateway.Application.Common.Interfaces;

namespace Checkout.Gateway.Application.Authentication.Queries
{
	public class GetMerchantUserTokenQueryHandler : IRequestHandler<GetMerchantUserTokenQuery, OneOf<GetTokenResponse, AuthenticationError>>
	{
		private readonly IAuthenticationClient _authenticationProvider;

		public GetMerchantUserTokenQueryHandler(IAuthenticationClient authenticationProvider)
		{
			_authenticationProvider = authenticationProvider;
		}

		public async Task<OneOf<GetTokenResponse, AuthenticationError>> Handle(GetMerchantUserTokenQuery query, CancellationToken cancellationToken)
		{
			var result = await _authenticationProvider.GetUserTokenAsync(query.UserName, query.Password);

			return result.Match<OneOf<GetTokenResponse, AuthenticationError>>(
				token => new GetTokenResponse(token), 
				error => error);
		}
	}
}
