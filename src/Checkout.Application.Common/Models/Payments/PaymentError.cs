namespace Checkout.Application.Common.Models.Payments
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
