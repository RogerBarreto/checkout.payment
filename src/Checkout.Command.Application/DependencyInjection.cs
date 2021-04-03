using System.Reflection;
using Checkout.Application.Common;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Checkout.Command.Application
{
	public static class DependencyInjection
	{
		public static void AddApplicationDependencies(this IServiceCollection services)
		{
			services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
			services.AddMediatR(Assembly.GetExecutingAssembly());
			services.AddApplicationCommonDependencies();
		}
	}
}
