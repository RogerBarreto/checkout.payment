using System;
using System.Net.Http;
using System.Threading.Tasks;
using Checkout.Payment.Processor.Seedwork.Interfaces;
using Checkout.Payment.Processor.Domain.Configurations;
using Checkout.Payment.Processor.Seedwork.Models;
using Microsoft.Extensions.Logging;
using Checkout.Payment.Processor.Seedwork.Extensions;
using Checkout.Payment.Processor.Domain.Models.PaymentCommand;
using System.Text.Json;
using Checkout.Payment.Processor.Domain.Interfaces;

namespace Checkout.Payment.Processor.Domain.HttpClients
{
    public class PaymentCommandHttpClientAdapter : BaseHttpClientAdapter, IPaymentCommandHttpClientAdapter
    {
        private readonly IDomainNotification _bus;

        public PaymentCommandHttpClientAdapter(HttpClient client, MicroServiceSettings microServiceSettings, ApplicationManifest manifest, IDomainNotification notificationBus, ILogger<PaymentCommandHttpClientAdapter> logger) : base(client, logger)
        {
            _bus = notificationBus;
            _httpClient.BaseAddress = new Uri(microServiceSettings.PaymentCommandBaseAddress);
            _httpClient.DefaultRequestHeaders.Add("User-Agent", $"{manifest.Name} {manifest.Version}");
        }

		public async Task<ITryResult<UpdatePaymentResponse>> TryUpdatePayment(UpdatePaymentRequest updateRequest)
		{
            var requestPayload = new
            {
                updateRequest.PaymentStatus,
                updateRequest.PaymentStatusDetails
            };

            var responseMessage = await TryPutJsonAsync($"{_httpClient.BaseAddress}v1/payment/{updateRequest.PaymentId}", requestPayload);
            if (!responseMessage.Success)
            {
                _bus.PublishError(responseMessage.Message);
                return TryResult<UpdatePaymentResponse>.CreateFailResult();
            }

            var responseContent = await responseMessage.Result.Content.ReadAsStringAsync();
            if (responseMessage.Result.IsSuccessStatusCode) 
            { 
                var updateResponse = JsonSerializer.Deserialize<UpdatePaymentResponse>(responseContent);
                
                _logger.LogInformation($"Update Payment Request Succeeded [PaymentId={updateRequest.PaymentId}, PaymentStatus={updateRequest.PaymentStatus}]");
                return TryResult<UpdatePaymentResponse>.CreateSuccessResult(updateResponse);
            }

            _logger.LogError($"Update Payment Failed [paymentId={updateRequest.PaymentId}, httpStatus={responseMessage.Result.StatusCode}, httpContent={responseContent}]");
            _bus.PublishBusinessViolation("Payment Request Failed");

            return TryResult<UpdatePaymentResponse>.CreateFailResult();
        }
    }
}
