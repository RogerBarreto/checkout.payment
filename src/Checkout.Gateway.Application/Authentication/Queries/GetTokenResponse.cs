namespace Checkout.Gateway.Application.Authentication.Queries
{
	public class GetTokenResponse
	{
		public string Token { get; }
		public GetTokenResponse(string token)
		{
			Token = token;
		}
	}
}
