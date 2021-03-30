using FluentValidation;

namespace Checkout.Gateway.Application.Authentication.Queries
{
	public class GetMerchantUserTokenQueryValidator : AbstractValidator<GetMerchantUserTokenQuery>
	{
		public GetMerchantUserTokenQueryValidator()
		{
			RuleFor(o => o.UserName)
				.NotNull()
				.NotEmpty();

			RuleFor(o => o.Password)
				.NotNull()
				.NotEmpty();

		}
	}
}
