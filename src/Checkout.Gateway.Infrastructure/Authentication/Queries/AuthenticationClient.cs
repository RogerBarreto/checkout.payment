using Checkout.Gateway.Application.Authentication.Errors;
using Checkout.Gateway.Application.Common.Interfaces;
using IdentityModel.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OneOf;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Checkout.Gateway.Infrastructure.Authentication.Queries
{
	public class AuthenticationClientAdapter : IAuthenticationClient
	{
		private readonly HttpClient _httpClient;
		private readonly ILogger<AuthenticationClientAdapter> _logger;
		private readonly string _clientId;
		private readonly string _clientSecret;

		public AuthenticationClientAdapter(HttpClient httpClient, IConfiguration configuration, ILogger<AuthenticationClientAdapter> logger)
		{
			_httpClient = httpClient;
			_logger = logger;
			_httpClient.BaseAddress = new Uri(configuration.GetSection("HttpClientSettings:Identity:BaseAddress").Value);
            _clientId = configuration.GetSection("HttpClientSettings:Identity:ClientId").Value;
            _clientSecret = configuration.GetSection("HttpClientSettings:Identity:ClientSecret").Value;
		}

        public async Task<OneOf<string, AuthenticationError>> GetApiTokenAsync(string apiKey, string apiSecret)
		{
            var discoResult = await GetDiscoveryStatus();
            if (discoResult.Value is AuthenticationError error) {
                return error;
            }

            var disco = discoResult.Value as DiscoveryResponse;
            var tokenResponse = await _httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = apiKey,
                ClientSecret = apiSecret,

                Scope = "merchant-api"
            });

            if (tokenResponse.IsError)
            {
                _logger.LogWarning($"Failed to get api token [apiKey={apiKey}, errorDescription={tokenResponse.ErrorDescription}]");
                return new AuthenticationError("Failed to get api token");
            }

            return tokenResponse.AccessToken;
        }

		public async Task<OneOf<string, AuthenticationError>> GetUserTokenAsync(string userName, string password)
		{
            var discoResult = await GetDiscoveryStatus();
            if (discoResult.Value is AuthenticationError error)
            {
                return error;
            }

            var disco = discoResult.Value as DiscoveryResponse;

            var tokenResponse = await _httpClient.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Password = password,
                UserName = userName,
                Address = disco.TokenEndpoint,
                ClientId = _clientId,
                ClientSecret = _clientSecret,
                Scope = "merchant",
            });

            if (tokenResponse.IsError)
            {
                _logger.LogWarning($"Failed to get token for user [UserName={userName}]");
                return new AuthenticationError($"Failed to get token for user");
            }

            return tokenResponse.AccessToken;
        }

        private async Task<OneOf<DiscoveryResponse, AuthenticationError>> GetDiscoveryStatus()
        {
            var disco = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {
                Address = _httpClient.BaseAddress.ToString(),
                Policy = { RequireHttps = false }
            });

            if (disco.IsError)
            {
                _logger.LogError($"Failed to contact Identity Server [reasonError={disco.Error}, identityBaseAddress={_httpClient.BaseAddress}]");
                return new AuthenticationError("Failed to contact Identity Server");
            }

            return disco;
        }
    }
}
