using System.Collections.Generic;
using System.Reflection;
using Checkout.WebApi.Common.Filters;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace Checkout.WebApi.Common.Extensions
{
    public static class FluentValidationExtensions
    {
        public static IMvcBuilder AddControllersWithFluentValidation(this IServiceCollection services, params Assembly[] assemblies)
        {
            var assemblyList = new List<Assembly>(assemblies);
            assemblyList.Add(Assembly.GetExecutingAssembly());
            
            return services.AddControllers(o =>
            {
                o.Filters.Add<ApiExceptionFilterAttribute>();
            }).AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblies(assemblyList));
        }
    }
}