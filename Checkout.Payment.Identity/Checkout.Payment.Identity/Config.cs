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
                new ApiScope("merchant", "Checkout.Payment.Gateway.MerchantUser"),
                new ApiScope("merchant-api", "Checkout.Payment.Gateway.MerchantApi")
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
                ClientName = "checkout.payment.gateway",
                ClientId = "checkout.payment.gateway",
                AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                AccessTokenType = AccessTokenType.Jwt,
                AccessTokenLifetime = 3600,
                IdentityTokenLifetime = 300,
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

            for (var i = 1; i < 100; i++)
            {
                yield return new Client
                {
                    ClientId = $"merchant.api.{i}.key",
                    ClientName = $"merchant{i}",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AccessTokenType = AccessTokenType.Jwt,
                    AccessTokenLifetime = 3600,
                    IdentityTokenLifetime = 300,
                    UpdateAccessTokenClaimsOnRefresh = false,
                    SlidingRefreshTokenLifetime = 30,
                    AllowOfflineAccess = true,
                    RefreshTokenUsage = TokenUsage.OneTimeOnly,
                    RefreshTokenExpiration = TokenExpiration.Sliding,
                    AlwaysSendClientClaims = true,
                    Enabled = true,
                    ClientSecrets = new List<Secret> { new Secret($"merchant.api.{i}.secret".Sha256()) },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        "merchant-api"
                    }
                };
            }
        }
    }
}
