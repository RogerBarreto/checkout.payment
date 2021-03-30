namespace Checkout.Gateway.Application.Common.Interfaces
{
	public interface ICreditCardValidator
	{
		bool Validate(string cardNumber);
	}
}
