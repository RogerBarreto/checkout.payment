using FluentValidation;

namespace Checkout.Gateway.Application.Authentication.Queries
{
	public class GetMerchantApiTokenQueryValidator : AbstractValidator<GetMerchantApiTokenQuery>
	{
		public GetMerchantApiTokenQueryValidator()
		{
			RuleFor(o => o.ApiSecret)
				.NotNull()
				.NotEmpty();

			RuleFor(o => o.ApiKey)
				.NotNull()
				.NotEmpty();

		}
	}
}
