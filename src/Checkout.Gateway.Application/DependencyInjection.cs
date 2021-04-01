using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Checkout.Gateway.Application
{
    public static class DependencyInjection
    {
        public static void AddApplicationDependencies(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
        }
    }
}