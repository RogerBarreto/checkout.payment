using Checkout.Identity.WebApi.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Checkout.Identity.WebApi.Extensions
{
    public static class CustomIdentityServerBuilderExtensions
    {
        public static IIdentityServerBuilder AddCustomUserStore(this IIdentityServerBuilder builder)
        {
            builder.AddProfileService<CustomProfileService>();
            builder.AddResourceOwnerValidator<CustomResourceOwnerPasswordValidator>();
            builder.AddCustomTokenRequestValidator<CustomResourceOwnerClientValidator>();

            return builder;
        }
    }
}
