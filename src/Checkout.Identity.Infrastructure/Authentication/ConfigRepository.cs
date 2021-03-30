using System.Collections.Generic;
using Checkout.Identity.Application.Authentication;
using IdentityServer4;
using IdentityServer4.Models;
namespace Checkout.Identity.Infrastructure.Authentication
{
    public class ConfigRepository : IConfigRepository
    {
        public IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
            {
                new ApiScope("merchant", "Checkout.Gateway.MerchantUser"),
                new ApiScope("merchant-api", "Checkout.Gateway.MerchantApi")
            };
        }

        public IEnumerable<Client> GetClients()
        {
            return CreateClients();
        }

        public IEnumerable<IdentityResource> GetIdentityResources()
        {
            yield return new IdentityResources.OpenId();
            yield return new IdentityResources.Profile();
            yield return new IdentityResources.Email();
        }
        
        private IEnumerable<Client> CreateClients()
        {
            yield return new Client
            {
                ClientName = "checkout.gateway.webapi",
                ClientId = "checkout.gateway.webapi",
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
                ClientSecrets = new List<Secret> { new Secret("checkout.gateway.webapi-secret".Sha256()) },
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