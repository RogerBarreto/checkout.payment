using Checkout.Identity.Application.Authentication;
using Checkout.Identity.WebApi.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Checkout.Identity.Infrastructure;
using Checkout.Identity.Infrastructure.Authentication;

namespace Checkout.Identity.WebApi
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddInfrastructureDependencies();

            var builder = IdentityServerBuilder(services);
            builder.AddDeveloperSigningCredential();
        }

        private static IIdentityServerBuilder IdentityServerBuilder(IServiceCollection services)
        {
            IConfigRepository identityConfig = new ConfigRepository();
            
            var builder = services.AddIdentityServer()
                .AddInMemoryIdentityResources(identityConfig.GetIdentityResources())
                .AddInMemoryApiScopes(identityConfig.GetApiScopes())
                .AddInMemoryClients(identityConfig.GetClients())
                .AddCustomUserStore();
            
            return builder;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseIdentityServer();
        }
    }
}
