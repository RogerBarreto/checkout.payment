using Checkout.Identity.Application.Authentication;
using Checkout.Identity.Infrastructure.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace Checkout.Identity.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructureDependencies(this IServiceCollection services)
        {
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<IApiKeyRepository, ApiKeyRepository>();
        }
    }
}