using Checkout.Payment.AcquiringBankMock.Application.Interfaces;
using Checkout.Payment.AcquiringBankMock.Application.Models;
using Checkout.Payment.AcquiringBankMock.Application.Services;
using Checkout.Payment.AcquiringBankMock.AuthenticationHandlers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;
using Swashbuckle.AspNetCore.Filters;

namespace Checkout.Payment.AcquiringBankMock
{
	public class Startup
	{
		public ApplicationManifest Manifest { get; }
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
			Manifest = Configuration.GetSection("ApplicationManifest").Get<ApplicationManifest>();
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{

			services.AddAuthentication("ApiKey").AddScheme<AuthenticationSchemeOptions, ApiKeyAuthenticationHandler>("ApiKey", o => { });
			services.AddScoped<IPaymentService, RandomPaymentService>();

			services.AddSwaggerGen(options =>
			{
				options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
				{
					Description = "Standard Authorization header using the Bearer scheme. Example: \"bearer {token}\"",
					In = ParameterLocation.Header,
					Name = "Authorization",
					Type = SecuritySchemeType.ApiKey,
					BearerFormat = "ApiKey",
					Scheme = "Bearer"
				});
				options.OperationFilter<SecurityRequirementsOperationFilter>();


				options.SwaggerDoc("v1", new OpenApiInfo
				{
					Title = Manifest.Name,
					Version = Manifest.Version
				});
			});

			services.AddRouting(options => options.LowercaseUrls = true);
			services.AddControllers();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			app.UseHttpsRedirection();

			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
			});

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
