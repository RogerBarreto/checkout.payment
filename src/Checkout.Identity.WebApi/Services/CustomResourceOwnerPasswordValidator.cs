using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Checkout.Identity.Application.Authentication;
using IdentityModel;
using IdentityServer4.Validation;

namespace Checkout.Identity.WebApi.Services
{
    public class CustomResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly IUserRepository _userRepository;

        public CustomResourceOwnerPasswordValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            if (_userRepository.ValidateCredentialsAsync(context.UserName, context.Password).Result)
            {
                var user = _userRepository.FindByUsernameAsync(context.UserName).Result;
                context.Result = new GrantValidationResult(user.Id, OidcConstants.AuthenticationMethods.Password);
            }

            return Task.FromResult(0);
        }
    }

    public class CustomResourceOwnerClientValidator : ICustomTokenRequestValidator
    {
        private readonly IApiKeyRepository _apiKeyRepository;

        public CustomResourceOwnerClientValidator(IApiKeyRepository apiRepository)
        {
            _apiKeyRepository = apiRepository;
        }

		public async Task ValidateAsync(CustomTokenRequestValidationContext context)
		{
            var client = context.Result.ValidatedRequest.Client;
            if (context.Result.ValidatedRequest.GrantType != "password")
            {
                var apiData = await _apiKeyRepository.FindByApiKeyAsync(client.ClientId);

                if (apiData == null)
                {
                    context.Result.IsError = true;
                    context.Result.Error = "No claims matched in repository to add";
                    return;
                }
                // get list of custom claims we want to add
                var claims = new List<Claim>
                {
                    new Claim("role", "payment.merchant"),
                    new Claim("username", apiData.UserName),
                    new Claim("email", apiData.Email),
                    new Claim("user_id", apiData.Id)
                };

                // add it
                claims.ToList().ForEach(u => context.Result.ValidatedRequest.ClientClaims.Add(u));

                context.Result.ValidatedRequest.Client.ClientClaimsPrefix = "";
            }
        }
	}
}
