namespace Checkout.WebApi.Common.Interfaces
{
	public interface IAuthenticationSettings
	{
		string Authority { get; set; }
		string ClientId { get; set; }
		string ClientSecret { get; set; }
	}
}
