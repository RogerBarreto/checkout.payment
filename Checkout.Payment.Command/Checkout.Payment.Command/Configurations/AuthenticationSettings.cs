using Checkout.Payment.Gateway.Seedwork.Interfaces;

namespace Checkout.Payment.Gateway.Configurations
{
    public class AuthenticationSettings : IAuthenticationSettings
    {
        public string Authority { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}
