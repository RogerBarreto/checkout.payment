using Checkout.Gateway.Application.Common.Interfaces;
using Checkout.Gateway.Infrastructure.Authentication.Queries;
using Checkout.Gateway.Infrastructure.Payments.Commands;
using Checkout.Gateway.Infrastructure.Payments.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace Checkout.Gateway.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructureDependencies(this IServiceCollection services)
        {
	        services.AddHttpClient<IPaymentCommandClient, PaymentCommandClientAdapter>();
	        services.AddHttpClient<IPaymentQueryClient, PaymentQueryClientAdapter>();
	        services.AddHttpClient<IAuthenticationClient, AuthenticationClientAdapter>();
        }
    }
}