using Checkout.Payment.Gateway.Configurations;
using Checkout.Payment.Gateway.Extensions;
using Checkout.Payment.Gateway.Seedwork.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.OpenApi.Models;
using Checkout.Payment.Gateway.Application.Services;
using Checkout.Payment.Gateway.Application.Interfaces;
using Checkout.Payment.Gateway.Seedwork.Models;
using Checkout.Payment.Gateway.MicroServices.Configurations;
using Checkout.Payment.Gateway.MicroServices.HttpClients;
using Microsoft.AspNetCore.Mvc;

namespace Checkout.Payment.Gateway
{
	public class Startup
    {
        public IConfiguration Configuration { get; }
        public ApplicationManifest Manifest { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Manifest = Configuration.GetSection("ApplicationManifest").Get<ApplicationManifest>();
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var authenticationSettings = Configuration.GetSection("AuthenticationSettings").Get<AuthenticationSettings>();
            var microserviceSettings = Configuration.GetSection("MicroServiceSettings").Get<MicroServiceSettings>();
            services.AddSingleton(Manifest);
            services.AddSingleton(microserviceSettings);
            services.AddSingleton<IAuthenticationSettings>(authenticationSettings);
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IDomainNotificationBus, DomainNotificationBus>();
            services.AddHttpClient<IPaymentIdentityHttpClientAdapter, PaymentIdentityHttpClientAdapter>();
            services.AddHttpClient<IPaymentCommandHttpClientAdapter, PaymentCommandHttpClientAdapter>();
            services.AddHttpClient<IPaymentQueryHttpClientAdapter, PaymentQueryHttpClientAdapter>();

            //Services
            services.AddScoped<IPaymentService, PaymentService>();


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

            services.AddJwtAuthNZ(authenticationSettings);
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "Standard Authorization header using the Bearer scheme. Example: \"bearer {token}\"",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
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


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });


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
