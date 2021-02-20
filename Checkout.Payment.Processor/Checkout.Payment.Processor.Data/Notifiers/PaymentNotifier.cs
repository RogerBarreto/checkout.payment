using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using Checkout.Payment.Processor.Data.Configurations;
using Checkout.Payment.Processor.Domain.Interfaces;
using Checkout.Payment.Processor.Domain.Models.Notification;
using Checkout.Payment.Processor.Seedwork.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Checkout.Payment.Processor.Data.Notifiers
{
	public class PaymentNotifier : IPaymentNotifier
	{
		private readonly ILogger<PaymentNotifier> _logger;
		private AmazonSimpleNotificationServiceClient _client;
		private SNSSettings _settings;
		public PaymentNotifier(IConfiguration configuration, ILogger<PaymentNotifier> logger)
		{
			_settings = configuration.GetSection("SNSSettings").Get<SNSSettings>();

			var serviceConfig = new AmazonSimpleNotificationServiceConfig()
			{
				ServiceURL = _settings.ServiceURL,
				UseHttp = _settings.UseHttp,
				ProxyHost = _settings.ProxyHost,
				ProxyPort = _settings.ProxyPort,
			};

			_client = new AmazonSimpleNotificationServiceClient(_settings.Credentials.AccessKey, _settings.Credentials.SecretKey, serviceConfig);
			_logger = logger;
		}

		public async Task<ITryResult<string>> TryReprocessPaymentAsync(ReprocessPaymentMessage message)
		{
			string messagePayload = null;
			try
			{
				messagePayload = JsonSerializer.Serialize(message);
				var publishRequest = new PublishRequest(_settings.PaymentProcessTopicArn, messagePayload);
				var publishResponse = await _client.PublishAsync(publishRequest);

				_logger.LogInformation($"Successfully sent payment back to reprocess [topicArn={_settings.PaymentProcessTopicArn}, messagePayload={messagePayload}]");
				return TryResult<string>.CreateSuccessResult(publishResponse.MessageId);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to send payment back to reprocess [topicArn={_settings.PaymentProcessTopicArn}, messagePayload={messagePayload}, exMessage={ex.Message}, exStack={ex.StackTrace}]");
				return TryResult<string>.CreateFailResult();
			}
		}
	}
}
