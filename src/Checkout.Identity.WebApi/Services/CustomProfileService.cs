using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Checkout.Identity.Application.Authentication;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.Extensions.Logging;

namespace Checkout.Identity.WebApi.Services
{
    public class CustomProfileService : IProfileService
    {
        private readonly ILogger Logger;
        private readonly IUserRepository _userRepository;

        public CustomProfileService(ILogger<CustomProfileService> logger, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            Logger = logger;
        }


        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var sub = context.Subject.GetSubjectId();

            Logger.LogDebug("Get profile called for subject {subject} from client {client} with claim types {claimTypes} via {caller}",
                context.Subject.GetSubjectId(),
                context.Client.ClientName ?? context.Client.ClientId,
                context.RequestedClaimTypes,
                context.Caller);

            var user = await _userRepository.FindBySubjectIdAsync(context.Subject.GetSubjectId());

            var claims = new List<Claim>
        {
            new Claim("role", "payment.merchant"),
            new Claim("username", user.UserName),
            new Claim("email", user.Email),
            new Claim("user_id", user.Id)
        };

            context.IssuedClaims = claims;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await _userRepository.FindBySubjectIdAsync(context.Subject.GetSubjectId());
            context.IsActive = user != null;
        }
    }
}
