using System.Reflection;
using Checkout.Command.Application;
using Checkout.Command.Infrastructure;
using Checkout.Command.WebApi.Configuration;
using Checkout.Infrastructure.Common.Configuration;
using Checkout.WebApi.Common.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Checkout.Command.WebApi
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		private IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddSingleton<ISnsSettings>(o => Configuration.GetSection("SnsSettings").Get<SnsSettings>());
			services.AddApplicationDependencies();
			services.AddInfrastructureDependencies(Configuration);
			services.AddSwaggerConfiguration();
			services.AddControllersWithFluentValidation(Assembly.GetAssembly(typeof(Startup)));
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseSwaggerConfiguration();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
