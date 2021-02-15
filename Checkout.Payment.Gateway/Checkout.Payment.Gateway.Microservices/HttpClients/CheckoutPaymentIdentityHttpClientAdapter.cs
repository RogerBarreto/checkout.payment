using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Checkout.Payment.Gateway.Seedwork.Interfaces;
using Checkout.Payment.Gateway.Microservices.Configurations;
using Checkout.Payment.Gateway.Seedwork.Models;

namespace Checkout.Payment.Gateway.Microservices.HttpClients
{
    public class CheckoutPaymentIdentityHttpClientAdapter
    {
        private readonly HttpClient _httpClient;
        private readonly MicroServiceSettings _microservicesSettings;
        private readonly IAuthenticationSettings _authenticationSettings;
        private readonly IDomainNotificationBus _bus;
        public CheckoutPaymentIdentityHttpClientAdapter(HttpClient client, MicroServiceSettings microseviceSettings, ApplicationManifest manifest, IAuthenticationSettings authenticationSettings, IDomainNotificationBus notificationBus)
        {
            _httpClient = client;
            _microservicesSettings = microseviceSettings;
            _authenticationSettings = authenticationSettings;
            _bus = notificationBus;
            _httpClient.BaseAddress = new Uri(_microservicesSettings.IdentityBaseUrl);
            _httpClient.DefaultRequestHeaders.Add("User-Agent", $"{manifest.Name} {manifest.Version}");
        }

        public async Task<string> GetTokenAsync(string userName, string password)
        {
            var disco = await _httpClient.GetDiscoveryDocumentAsync(_httpClient.BaseAddress.ToString());
            if (disco.IsError)
            {
                _bus.PublishError(disco.Error);
                return null;
            }

            // request token
            var tokenResponse = await _httpClient.RequestPasswordTokenAsync(new PasswordTokenRequest { 
                Password = password, 
                UserName = userName,
                Address = disco.TokenEndpoint,
                ClientId = _authenticationSettings.ClientId,
                ClientSecret = _authenticationSettings.ClientSecret,
                Scope = "merchant",
            });

            if (tokenResponse.IsError)
            {
                _bus.PublishBusinessViolation(disco.Error);
                return null;
            }

            return tokenResponse.AccessToken;
        }
    }
}
