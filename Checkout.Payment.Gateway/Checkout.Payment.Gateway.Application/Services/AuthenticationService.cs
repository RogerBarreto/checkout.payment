using Checkout.Payment.Gateway.Application.Interfaces;
using Checkout.Payment.Gateway.Microservices.HttpClients;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Checkout.Payment.Gateway.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ILogger<AuthenticationService> _logger;
        private readonly CheckoutPaymentIdentityHttpClientAdapter _identityClient;
        public AuthenticationService(ILogger<AuthenticationService> logger, CheckoutPaymentIdentityHttpClientAdapter identityClient)
        {
            _identityClient = identityClient;
            _logger = logger;
        }

        public async Task<string> LoginGetTokenAsync(UserTokenRequestModel loginRequestModel)
        {
            return await _identityClient.GetTokenAsync(loginRequestModel.UserName, loginRequestModel.Password);
        }
    }
}
