using System;
using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;
using static IdentityServer4.Models.IdentityResources;

namespace Checkout.Payment.Identity
{
    public static class Config
    {
        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("merchant", "Checkout.Payment.Gateway")
            };

        public static IEnumerable<Client> Clients => CreateClients();

        public static IEnumerable<IdentityResource> IdentityResources => CreateIdentityResources();

        private static IEnumerable<IdentityResource> CreateIdentityResources()
        {
            yield return new OpenId();
            yield return new Profile();
            yield return new Email();
        }

        private static IEnumerable<Client> CreateClients()
        {
            yield return new Client
            {
                ClientId = "checkout.payment.gateway",
                AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                AccessTokenType = AccessTokenType.Jwt,
                AccessTokenLifetime = 120,
                IdentityTokenLifetime = 120,
                UpdateAccessTokenClaimsOnRefresh = false,
                SlidingRefreshTokenLifetime = 30,
                AllowOfflineAccess = true,
                RefreshTokenUsage = TokenUsage.OneTimeOnly,
                RefreshTokenExpiration = TokenExpiration.Sliding,
                AlwaysSendClientClaims = true,
                Enabled = true,
                ClientSecrets = new List<Secret> { new Secret("checkout.payment.gateway-secret".Sha256()) },
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    IdentityServerConstants.StandardScopes.OfflineAccess,
                    "merchant"
                }
            };
        }
    }
}
