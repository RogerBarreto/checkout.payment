namespace Checkout.Application.Common.Models.Authentication
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
