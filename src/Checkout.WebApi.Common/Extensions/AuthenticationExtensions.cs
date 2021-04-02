using System;
using System.Text;
using Checkout.WebApi.Common.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Checkout.WebApi.Common.Extensions
{
    public static class AuthenticationExtensions
    {
        public static void AddJwtAuthNZ(this IServiceCollection services, IAuthenticationSettings authenticationSettings)
        {
            // accepts any access token issued by identity server
            services
                .AddAuthentication(o => {
                    o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer("Bearer", options =>
                {
                    options.SaveToken = false;
                    options.Authority = authenticationSettings.Authority;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = authenticationSettings.Authority,

                        ValidateAudience = false,
                        ValidateIssuerSigningKey = false,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(authenticationSettings.ClientSecret)),
                        //comment this and add this line to fool the validation logic
                        /*SignatureValidator = delegate (string token, TokenValidationParameters parameters)
                        {
                            var jwt = new JwtSecurityToken(token);

                            return jwt;
                        },*/

                        RequireExpirationTime = true,
                        ValidateLifetime = true,

                        ClockSkew = TimeSpan.Zero,

                        RequireSignedTokens = false
                    };

                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiScope", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "merchant", "merchant-api");
                });
            });
        }
    }
}
