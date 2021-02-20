using System;
using System.IO;
using Serilog;
using Serilog.Core;
using MediatR;
using Checkout.Payment.Processor.Seedwork.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Checkout.Payment.Processor.Application.Interfaces;
using Checkout.Payment.Processor.Application.Services;
using Checkout.Payment.Processor.Domain.Interfaces;
using Checkout.Payment.Processor.Domain.HttpClients;
using Checkout.Payment.Processor.Seedwork.Extensions;
using Checkout.Payment.Processor.Domain.CommandHandlers;
using Checkout.Payment.Processor.Data.Notifiers;
using Checkout.Payment.Processor.Domain.Commands;
using Checkout.Payment.Processor.MicroServices.Configurations;
using System.Text.Json;

namespace Checkout.Payment.Processor
{
	public static class Startup
	{
		private static IConfiguration _configuration;
		private static ApplicationManifest _manifest;
		private static ServiceCollection _serviceCollection;
		private static ServiceProvider _serviceProvider;
		private static bool deserializeCasesensitiveApplied = false;
		public static void ConfigureServices()
		{
			DeserializeCaseInsensitiveConfig();

			IConfigurationBuilder builder = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile($"manifest.json", optional: false)
				.AddJsonFile($"appsettings.json", optional: false);

			_configuration = builder.Build();
			_manifest = _configuration.GetSection("ApplicationManifest").Get<ApplicationManifest>();
			var microServiceSettings = _configuration.GetSection("MicroServiceSettings").Get<MicroServiceSettings>();

			Logger Logger = new LoggerConfiguration()
		   .ReadFrom.Configuration(_configuration)
		   .Enrich.FromLogContext()
		   //.WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}", theme: AnsiConsoleTheme.Code)
		   .CreateLogger();

			_serviceCollection = new ServiceCollection();
			var services = _serviceCollection;

			services.AddMediatR(typeof(Startup));
			services.AddSingleton(_configuration);
			services.AddSingleton(_manifest);
			services.AddSingleton(microServiceSettings);
			services.AddScoped<IPaymentService, PaymentService>();
			services.AddScoped<IPaymentNotifier, PaymentNotifier>();

			services.AddHttpClient<IPaymentCommandHttpClientAdapter, PaymentCommandHttpClientAdapter>();
			services.AddHttpClient<IAcquiringBankHttpClientAdapter, AcquiringBankMockHttpClientAdapter>();

			services.AddScoped<IRequestHandler<SendBankPaymentCommand, ITryResult<SendBankPaymentCommandResponse>>, PaymentCommandHandler>();
			services.AddScoped<IRequestHandler<UpdatePaymentCommand, ITryResult>, PaymentCommandHandler>();
			services.AddScoped<IRequestHandler<ReprocessPaymentCommand, ITryResult>, PaymentCommandHandler>();

			services.AddLogging(b =>
			{
				b.ClearProviders();
				b.AddSerilog(Logger);
			});
		}

		private static void DeserializeCaseInsensitiveConfig()
		{
			if (!deserializeCasesensitiveApplied)
			{
				((JsonSerializerOptions)typeof(JsonSerializerOptions)
								.GetField("s_defaultOptions", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic).GetValue(null))
							.PropertyNameCaseInsensitive = true;

				deserializeCasesensitiveApplied = true;
			}
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
