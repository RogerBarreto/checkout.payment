namespace Checkout.Gateway.Application.Payments.Errors
{
	public struct PaymentError 
	{
		public string Message { get; }
		public PaymentError(string message)
		{
			Message = message;
		}
	}
}
