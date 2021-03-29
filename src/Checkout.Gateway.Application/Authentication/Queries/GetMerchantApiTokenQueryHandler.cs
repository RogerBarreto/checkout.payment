using Checkout.Gateway.Application.Authentication.Errors;
using Checkout.Gateway.Application.Common.Interfaces;
using MediatR;
using OneOf;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Gateway.Application.Authentication.Queries
{
	public class GetMerchantApiTokenQueryHandler : IRequestHandler<GetMerchantApiTokenQuery, OneOf<GetTokenResponse, AuthenticationError>>
	{
		private readonly IAuthenticationClient _authenticationProvider;

		public GetMerchantApiTokenQueryHandler(IAuthenticationClient authenticationProvider)
		{
			_authenticationProvider = authenticationProvider;
		}
		public async Task<OneOf<GetTokenResponse, AuthenticationError>> Handle(GetMerchantApiTokenQuery query, CancellationToken cancellationToken)
		{
			var result = await _authenticationProvider.GetApiTokenAsync(query.UserName, query.ApiKey);

			return result.Match<OneOf<GetTokenResponse, AuthenticationError>>(
				token => new GetTokenResponse(token), 
				error => error);
		}
	}
}
