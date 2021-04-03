using Checkout.Application.Common.Interfaces;

namespace Checkout.Infrastructure.Common.Validators
{
	public class CreditCardValidator : ICreditCardValidator
	{
		public bool Validate(string cardNumber)
		{
			return true;
		}
	}
}
