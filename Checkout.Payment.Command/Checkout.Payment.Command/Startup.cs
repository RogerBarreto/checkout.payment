using Checkout.Payment.Command.Seedwork.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Checkout.Payment.Command.Application.Services;
using Checkout.Payment.Command.Application.Interfaces;
using Checkout.Payment.Command.Seedwork.Models;
using MediatR;
using System.Reflection;
using Checkout.Payment.Command.Domain;
using Checkout.Payment.Command.Seedwork.Extensions;
using Checkout.Payment.Command.Domain.CommandHandlers;
using Checkout.Payment.Command.Domain.Interfaces;
using Checkout.Payment.Command.Data;
using Checkout.Payment.Command.Data.Notifiers;

namespace Checkout.Payment.Command
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
            services.AddScoped<IPaymentNotifier, PaymentNotifier>();
            services.AddScoped<IRequestHandler<CreatePaymentCommand, ITryResult<CreatePaymentCommandResponse>>, PaymentCommandHandler>();
            services.AddScoped<IRequestHandler<UpdatePaymentCommand, ITryResult<UpdatePaymentCommandResponse>>, PaymentCommandHandler>();

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = Configuration.GetConnectionString("PaymentCache");
            });
            services.AddControllers();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = Manifest.Name,
                    Version = Manifest.Version
                });
            });
        }
       
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
                c.SwaggerEndpoint("/swagger/v1/swagger.json", Manifest.Name);
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
