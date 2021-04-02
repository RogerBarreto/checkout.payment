using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using Checkout.Gateway.Application;
using Checkout.Gateway.Infrastructure;
using Checkout.Gateway.WebApi.Models;
using Checkout.WebApi.Common.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

namespace Checkout.Gateway.WebApi
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddApplicationDependencies();
			services.AddInfrastructureDependencies();

			services.AddApiVersioning(options =>
			{
				options.DefaultApiVersion = new ApiVersion(1, 0);
				options.AssumeDefaultVersionWhenUnspecified = true;
				options.ReportApiVersions = true;
			});
			services.AddVersionedApiExplorer(options => {
				options.GroupNameFormat = "'v'VVV";
				options.DefaultApiVersion = new ApiVersion(1, 0);
				options.AssumeDefaultVersionWhenUnspecified = true;
				options.SubstituteApiVersionInUrl = true;
			});

			services.AddJwtAuthNZ(Configuration.GetSection("HttpClientSettings:Identity").Get<AuthenticationSettings>());
			services.AddSwaggerConfigurationWithAuthentication();
			services.AddRouting(options => options.LowercaseUrls = true);

			services.AddControllers();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseSwaggerConfiguration();

			//app.UseHttpsRedirection();

			app.UseAuthentication();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}

		
	}
}
