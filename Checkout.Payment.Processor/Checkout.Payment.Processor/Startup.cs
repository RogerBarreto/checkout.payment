using Checkout.Payment.Processor.Seedwork.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Serilog;
using Serilog.Core;
using MediatR;
using Checkout.Payment.Processor.Seedwork.Interfaces;
using Checkout.Payment.Processor.Application.Interfaces;
using Checkout.Payment.Processor.Application.Services;

namespace Checkout.Payment.Processor
{
	public static class Startup
	{
		private static IConfiguration _configuration;
		private static ApplicationManifest _manifest;
		private static ServiceCollection _serviceCollection;
		private static ServiceProvider _serviceProvider;

		public static void ConfigureServices()
        {

            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.json", optional: true);

            _configuration = builder.Build();
            _manifest = _configuration.GetSection("Manifest").Get<ApplicationManifest>();

            Logger Logger = new LoggerConfiguration()
           .ReadFrom.Configuration(_configuration)
           .Enrich.FromLogContext()
           //.WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}", theme: AnsiConsoleTheme.Code)
           .CreateLogger();

            _serviceCollection = new ServiceCollection();
            var services = _serviceCollection;

            services.AddMediatR(typeof(Startup));
            services.AddSingleton(_configuration);

            services.AddScoped<IDomainNotification, DomainNotification>();
            services.AddScoped<IPaymentService, PaymentService>();

            services.AddLogging(b => 
            { 
                b.ClearProviders();
                b.AddSerilog(Logger);
            });
        }

        public static IServiceProvider GetServiceProvider()
		{
            if (_serviceProvider == null)
			{
                _serviceProvider = _serviceCollection.BuildServiceProvider();
			}

            return _serviceProvider;
		}
    }
}
