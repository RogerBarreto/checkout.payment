using System.Reflection;
using Checkout.Application.Common.Behaviours;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using MediatR;

namespace Checkout.Application.Common
{
    public static class DependencyInjection
    {
        public static void AddApplicationCommonDependencies(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        }
    }
}