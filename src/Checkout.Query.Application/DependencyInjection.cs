using System.Reflection;
using Checkout.Application.Common.Models.Payments.Queries;
using Checkout.Query.Application.Payments.Queries;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Checkout.Query.Application
{
	public static class DependencyInjection
	{
		public static void AddApplicationDependencies(this IServiceCollection services)
		{
			services.AddMediatR(Assembly.GetExecutingAssembly(), Assembly.GetAssembly(typeof(GetPaymentQueryHandler)), Assembly.GetAssembly(typeof(GetPaymentQuery)));
		}
	}
}
