using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using Checkout.Payment.Command.Data.Configurations;
using Checkout.Payment.Command.Domain.Interfaces;
using Checkout.Payment.Command.Domain.Models;
using Checkout.Payment.Command.Seedwork.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Checkout.Payment.Command.Data.Notifiers
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

		public async Task<ITryResult<string>> TryNotifyPaymentAsync(PaymentRequest request)
		{
			string messagePayload = null;
			try
			{
				messagePayload = JsonSerializer.Serialize(request);
				var publishRequest = new PublishRequest(_settings.NotifyPaymentTopicArn, messagePayload);
				var publishResponse = await _client.PublishAsync(publishRequest);

				_logger.LogInformation($"Success to publish message to topic [topicArn={_settings.NotifyPaymentTopicArn}, messagePayload={messagePayload}]");

				return TryResult<string>.CreateSuccessResult(publishResponse.MessageId);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to publish message to topic [topicArn={_settings.NotifyPaymentTopicArn}, messagePayload={messagePayload}, exMessage={ex.Message}, exStack={ex.StackTrace}]");

				return TryResult<string>.CreateFailResult();
			}
		}
	}
}
