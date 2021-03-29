using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Checkout.Gateway.Application.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static void AddApplicationDependencies(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
        }
    }
}