namespace Checkout.Application.Common.Interfaces
{
	public interface ICreditCardValidator
	{
		bool Validate(string cardNumber);
	}
}
