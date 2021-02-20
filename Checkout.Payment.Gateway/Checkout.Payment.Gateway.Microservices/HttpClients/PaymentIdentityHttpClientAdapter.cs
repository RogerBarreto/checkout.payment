using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Checkout.Payment.Gateway.Seedwork.Interfaces;
using Checkout.Payment.Gateway.MicroServices.Configurations;
using Checkout.Payment.Gateway.Seedwork.Models;
using Microsoft.Extensions.Logging;

namespace Checkout.Payment.Gateway.MicroServices.HttpClients
{
    public class PaymentIdentityHttpClientAdapter : IPaymentIdentityHttpClientAdapter
    {
        private readonly HttpClient _httpClient;
        private readonly IAuthenticationSettings _authenticationSettings;
        private readonly IDomainNotificationBus _bus;
		private readonly ILogger<PaymentIdentityHttpClientAdapter> _logger;

		public PaymentIdentityHttpClientAdapter(HttpClient client, MicroServiceSettings microSeviceSettings, ApplicationManifest manifest, IAuthenticationSettings authenticationSettings, IDomainNotificationBus notificationBus, ILogger<PaymentIdentityHttpClientAdapter> logger)
        {
            _bus = notificationBus;
			_logger = logger;
			_httpClient = client;
            _authenticationSettings = authenticationSettings;

            _httpClient.BaseAddress = new Uri(microSeviceSettings.IdentityBaseAddress);
            _httpClient.DefaultRequestHeaders.Add("User-Agent", $"{manifest.Name} {manifest.Version}");
        }

        public async Task<string> GetTokenAsync(string userName, string password)
        {
            var disco = await _httpClient.GetDiscoveryDocumentAsync(_httpClient.BaseAddress.ToString());
            if (disco.IsError)
            {
                _logger.LogError($"Failed to get contact Identity Server [reasonError={disco.Error}, identityBaseAddress={_httpClient.BaseAddress}]");
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
                _logger.LogInformation($"Failed to get token for user [UserName={userName}]");
                _bus.PublishBusinessViolation($"{tokenResponse.Error} {tokenResponse.ErrorDescription}");
                return null;
            }

            return tokenResponse.AccessToken;
        }
    }
}
