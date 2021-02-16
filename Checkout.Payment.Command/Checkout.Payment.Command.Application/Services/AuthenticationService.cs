using Checkout.Payment.Gateway.Application.Interfaces;
using Checkout.Payment.Gateway.MicroServices.HttpClients;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Checkout.Payment.Gateway.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ILogger<AuthenticationService> _logger;
        private readonly IPaymentIdentityHttpClientAdapter _identityClient;
        public AuthenticationService(ILogger<AuthenticationService> logger, IPaymentIdentityHttpClientAdapter identityClient)
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
