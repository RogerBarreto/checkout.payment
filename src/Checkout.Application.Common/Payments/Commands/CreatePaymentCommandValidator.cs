using Checkout.Domain.Common;
using FluentValidation;
using System;
using System.Linq;
using Checkout.Application.Common.Interfaces;

namespace Checkout.Application.Common.Payments.Commands
{
	public class CreatePaymentCommandValidator : AbstractValidator<CreatePaymentCommand>
	{
       public CreatePaymentCommandValidator(IDateTime currentDateTime, ICreditCardValidator creditCardValidator)
		{

			RuleFor(o => o.MerchantId).GreaterThan(0);
			RuleFor(o => o.CardNumber).Must(creditCardValidator.Validate).WithMessage("Invalid credit card number");
			RuleFor(o => o.CardCVV).InclusiveBetween(100, 999);
			RuleFor(o => o.Amount).GreaterThan(0);

			ValidateCurrencyType();
			ValidateCardExpiry(currentDateTime);
		}

		private void ValidateCurrencyType()
		{
			RuleFor(o => o.CurrencyType).Must(currencyValue =>
			{
				var currencyEnumsAsString = ((Domain.Enums.CurrencyType[])Enum.GetValues(typeof(Domain.Enums.CurrencyType)))
								.Select(x => x.ToString());

				return currencyEnumsAsString.Any(c => string.Equals(c, currencyValue, StringComparison.InvariantCultureIgnoreCase));
			});
		}

		private void ValidateCardExpiry(IDateTime currentDateTime)
		{
			var initialExpiryDate = currentDateTime.Value.AddMonths(1);

			RuleFor(o => o.CardExpiryYear).InclusiveBetween(initialExpiryDate.Year, initialExpiryDate.Year + 20);

			RuleFor(o => o.CardExpiryMonth)
				.InclusiveBetween(1, 12)
				.When(o => o.CardExpiryYear < initialExpiryDate.Year);

			RuleFor(o => o.CardExpiryMonth)
				.InclusiveBetween(initialExpiryDate.Month, 12)
				.When(o => o.CardExpiryYear == initialExpiryDate.Year);

			RuleFor(o => o.CardExpiryMonth)
				.InclusiveBetween(1, initialExpiryDate.AddMonths(-1).Month)
				.When(o => o.CardExpiryYear == initialExpiryDate.Year + 20);
		}
    }
}
