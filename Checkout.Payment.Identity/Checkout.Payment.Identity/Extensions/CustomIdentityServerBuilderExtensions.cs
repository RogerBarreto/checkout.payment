using Checkout.Payment.Identity.Data.Repositories;
using Checkout.Payment.Identity.Domain.Interfaces;
using Checkout.Payment.Identity.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Checkout.Payment.Identity.Extensions
{
    public static class CustomIdentityServerBuilderExtensions
    {
        public static IIdentityServerBuilder AddCustomUserStore(this IIdentityServerBuilder builder)
        {
            builder.Services.TryAddSingleton<IUserRepository, UserRepository>();
            builder.Services.TryAddSingleton<IApiKeyRepository, ApiKeyRepository>();
            builder.AddProfileService<CustomProfileService>();
            builder.AddResourceOwnerValidator<CustomResourceOwnerPasswordValidator>();
            builder.AddCustomTokenRequestValidator<CustomResourceOwnerClientValidator>();

            return builder;
        }
    }
}
