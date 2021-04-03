using Checkout.Application.Common.Interfaces;
using Checkout.Domain.Common;
using Checkout.Infrastructure.Common.Providers;
using Checkout.Infrastructure.Common.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace Checkout.Infrastructure.Common
{
    public static class DependencyInjection
    {
        public static void AddInfrastructureCommonDependencies(this IServiceCollection services)
        {
            services.AddSingleton<IDateTime, DateTimeProvider>();
            services.AddSingleton<ICreditCardValidator, CreditCardValidator>();
        }
    }
}