using Checkout.Payment.Query.Seedwork.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Checkout.Payment.Query.Application.Services;
using Checkout.Payment.Query.Application.Interfaces;
using Checkout.Payment.Query.Seedwork.Models;
using MediatR;
using System.Reflection;
using Checkout.Payment.Query.Domain;
using Checkout.Payment.Query.Seedwork.Extensions;
using Checkout.Payment.Query.Domain.CommandHandlers;
using Checkout.Payment.Query.Domain.Interfaces;
using Checkout.Payment.Query.Data;
using Microsoft.AspNetCore.Mvc;

namespace Checkout.Payment.Query
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
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddSingleton(Manifest);
            services.AddScoped<IDomainNotification, DomainNotification>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IRequestHandler<GetPaymentQuery, ITryResult<GetPaymentQueryResponse>>, PaymentQueryHandler>();

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = Configuration.GetConnectionString("PaymentCache");
            });
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

            services.AddSwaggerGen(options =>
            {
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

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
