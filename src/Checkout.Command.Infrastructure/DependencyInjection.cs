using Checkout.Command.Application.Common.Interfaces;
using Checkout.Command.Infrastructure.Payments;
using Checkout.Infrastructure.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Checkout.Command.Infrastructure
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
			services.AddScoped<IPaymentNotifier, PaymentNotifier>();
			services.AddInfrastructureCommonDependencies();
		}
	}
}
