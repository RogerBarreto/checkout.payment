using System;
using System.Threading.Tasks;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using Checkout.Application.Common.Models.Payments.Commands;
using Checkout.Application.Common.Models.Shared;
using Checkout.Command.Application.Common.Interfaces;
using Checkout.Infrastructure.Common.Configuration;
using Checkout.Infrastructure.Common.Extensions;
using Microsoft.Extensions.Logging;
using OneOf;

namespace Checkout.Command.Infrastructure.Payments
{
    internal class PaymentNotifier : IPaymentNotifier
    {
        private readonly ILogger<PaymentNotifier> _logger;
        private readonly ISnsSettings _settings;
        private readonly AmazonSimpleNotificationServiceClient _client;

        public PaymentNotifier(ILogger<PaymentNotifier> logger, ISnsSettings settings)
        {
            _logger = logger;
            _settings = settings;

            var serviceConfig = new AmazonSimpleNotificationServiceConfig()
            {
                ServiceURL = settings.ServiceURL,
                UseHttp = settings.UseHttp,
                ProxyHost = settings.ProxyHost,
                ProxyPort = settings.ProxyPort,
            };
            
            _client = new AmazonSimpleNotificationServiceClient(settings.AccessKey, settings.SecretKey, serviceConfig);
        }

        public async Task<OneOf<NotificationSuccess, NotificationError>> NotifyCreatePaymentAsync(CreatePaymentCommand command)
        {
            string messagePayload = null;
            try
            {
                messagePayload = JsonSerializer.Serialize(command);
                var publishRequest = new PublishRequest(_settings.NotifyPaymentTopicArn, messagePayload);
                var publishResponse = await _client.PublishAsync(publishRequest);

                _logger.LogInformation($"Success to publish message to topic [topicArn={_settings.NotifyPaymentTopicArn}, messagePayload={messagePayload}]");

                return new NotificationSuccess(publishResponse.MessageId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to publish message to topic [topicArn={_settings.NotifyPaymentTopicArn}, messagePayload={messagePayload}, exMessage={ex.Message}, exStack={ex.StackTrace}]");

                return new NotificationError("Failed to publish message to topic");
            }
        }
    }
}