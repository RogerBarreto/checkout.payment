using FluentValidation;
using MediatR;

namespace Checkout.Gateway.Application.Authentication.Queries
{
	public class GetMerchantApiTokenQueryValidator : AbstractValidator<GetMerchantApiTokenQuery>
	{
		public GetMerchantApiTokenQueryValidator()
		{
			RuleFor(o => o.UserName)
				.NotNull()
				.NotEmpty();

			RuleFor(o => o.ApiKey)
				.NotNull()
				.NotEmpty();

		}
	}
}
