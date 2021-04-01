namespace Checkout.Domain.Errors
{
	public struct AuthenticationError
	{
		public string Message { get; }
		public AuthenticationError(string message)
		{
			Message = message;
		}
	}
}
