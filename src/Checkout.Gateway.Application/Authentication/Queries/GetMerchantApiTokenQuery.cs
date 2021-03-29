﻿using Checkout.Gateway.Application.Authentication.Errors;
using MediatR;
using OneOf;

namespace Checkout.Gateway.Application.Authentication.Queries
{
	public class GetMerchantApiTokenQuery : IRequest<OneOf<GetTokenResponse, AuthenticationError>>
	{
		public string UserName { get; set; }
		public string ApiKey { get; set; }

	}
}