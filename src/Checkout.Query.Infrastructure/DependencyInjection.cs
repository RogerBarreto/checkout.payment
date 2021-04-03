using Checkout.Infrastructure.Common;
using Checkout.Query.Application.Common.Interfaces;
using Checkout.Query.Infrastructure.Payments;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Checkout.Query.Infrastructure
{
	public static class DependencyInjection
	{
		public static void AddInfrastructureDependencies(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddStackExchangeRedisCache(options =>
			{
				options.Configuration = configuration.GetConnectionString("PaymentCache");
			});

			services.AddScoped<IPaymentRepository, PaymentRepository>();
			services.AddInfrastructureCommonDependencies();
		}
	}
}
