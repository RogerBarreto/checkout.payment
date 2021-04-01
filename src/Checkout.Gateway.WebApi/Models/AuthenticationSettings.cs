using Checkout.WebApi.Common.Interfaces;

namespace Checkout.Gateway.WebApi.Models
{
	public class AuthenticationSettings : IAuthenticationSettings
	{
		public string Authority { get; set; }
		public string ClientId { get; set; }
		public string ClientSecret { get; set; }
	}
}
