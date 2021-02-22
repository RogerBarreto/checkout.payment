using Checkout.Payment.Gateway.Application.Interfaces;
using Checkout.Payment.Gateway.MicroServices.HttpClients;
using System.Threading.Tasks;

namespace Checkout.Payment.Gateway.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IPaymentIdentityHttpClientAdapter _identityClient;
        public AuthenticationService(IPaymentIdentityHttpClientAdapter identityClient)
        {
            _identityClient = identityClient;
        }

		public async Task<string> ApiGetTokenAsync(ApiTokenRequestModel loginRequestModel)
		{
            return await _identityClient.GetApiTokenAsync(loginRequestModel.ApiKey, loginRequestModel.ApiSecret);
        }

        public async Task<string> UserGetTokenAsync(UserTokenRequestModel loginRequestModel)
        {
            return await _identityClient.GetUserTokenAsync(loginRequestModel.UserName, loginRequestModel.Password);
        }
    }
}
