namespace Checkout.Infrastructure.Common.HttpClients.Errors
{
	public struct HttpCallError
	{
		public string Message { get;  }
		public HttpCallError(string message)
		{
			Message = message;
		}
	}
}
