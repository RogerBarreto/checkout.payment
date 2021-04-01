using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace Checkout.WebApi.Common.Extensions
{
	public static class SwaggerExtensions
	{
		public static void AddSwaggerConfiguration(this IServiceCollection services)
		{
			services.AddSwaggerGen(o =>
			{
				o.SwaggerDoc("v1",
					new OpenApiInfo
					{
						Title = GetApplicationName(),
						Version = GetApplicationVersion()
					});
			});
		}

		public static void AddSwaggerConfigurationWithAuthentication(this IServiceCollection services)
		{
			services.AddSwaggerGen(o =>
			{
				o.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
				{
					Description = "Standard Authorization header using the Bearer scheme. Example: \"bearer {token}\"",
					In = ParameterLocation.Header,
					Name = "Authorization",
					Type = SecuritySchemeType.Http,
					BearerFormat = "JWT",
					Scheme = "Bearer"
				});

				o.OperationFilter<SecurityRequirementsOperationFilter>();

				o.SwaggerDoc("v1",
					new OpenApiInfo
					{
						Title = GetApplicationName(),
						Version = GetApplicationVersion()
					});
			});
		}


		public static void UseSwaggerConfiguration(this IApplicationBuilder app)
		{
			app.UseSwagger();
			app.UseSwaggerUI(o => o.SwaggerEndpoint("v1/swagger.json", GetApplicationName()));
		}

		private static string GetApplicationName()
		{
			return Assembly.GetEntryAssembly()?.GetName().Name;
		}
		private static string GetApplicationVersion()
		{
			return Assembly.GetEntryAssembly()?.GetName().Version?.ToString();
		}

		
	}
}
