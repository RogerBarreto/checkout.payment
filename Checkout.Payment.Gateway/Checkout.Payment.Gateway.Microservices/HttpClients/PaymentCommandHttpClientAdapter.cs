using System;
using System.Net.Http;
using System.Threading.Tasks;
using Checkout.Payment.Gateway.Seedwork.Interfaces;
using Checkout.Payment.Gateway.MicroServices.Configurations;
using Checkout.Payment.Gateway.Seedwork.Models;
using Newtonsoft.Json;
using Checkout.Payment.Gateway.Domain.Models;
using Microsoft.Extensions.Logging;
using Checkout.Payment.Gateway.Seedwork.Extensions;
using System.Collections.Specialized;

namespace Checkout.Payment.Gateway.MicroServices.HttpClients
{
    public class PaymentCommandHttpClientAdapter : BaseHttpClientAdapter, IPaymentCommandHttpClientAdapter
    {
        private readonly IDomainNotificationBus _bus;

        public PaymentCommandHttpClientAdapter(HttpClient client, MicroServiceSettings microServiceSettings, ApplicationManifest manifest, IDomainNotificationBus notificationBus, ILogger<PaymentCommandHttpClientAdapter> logger) : base(client, logger)
        {
            _bus = notificationBus;
            _httpClient.BaseAddress = new Uri(microServiceSettings.PaymentCommandBaseAddress);
            _httpClient.DefaultRequestHeaders.Add("User-Agent", $"{manifest.Name} {manifest.Version}");
        }

        public async Task<ITryResult<CreatePaymentResult>> TryCreatePayment(CreatePayment createRequest)
        {
            var requestPayload = new
            {
                createRequest.Amount,
                createRequest.CardNumber,
                createRequest.CardCVV,
                createRequest.ExpiryDate,
                CurrencyType = createRequest.CurrencyType.ToString(),
            };

            var responseMessage = await TryPostJsonAsync($"{_httpClient.BaseAddress}payment", requestPayload, new NameValueCollection { { "MerchantId", createRequest.MerchantId.ToString() } });

            if (!responseMessage.Success)
            {
                _bus.PublishError(responseMessage.Message);
                return TryResult<CreatePaymentResult>.CreateFailResult();
            }

            var responseContent = await responseMessage.Result.Content.ReadAsStringAsync();
            if (responseMessage.Result.IsSuccessStatusCode)
            {
                var paymentResponse = JsonConvert.DeserializeObject<CreatePaymentResult>(responseContent);
                if (paymentResponse != null)
                {
                    _logger.LogInformation($"Payment Request Succeeded [PaymentId={paymentResponse.PaymentId}]");
                    return TryResult<CreatePaymentResult>.CreateSuccessResult(paymentResponse);
                }

                _logger.LogError($"Payment Response Failed [merchantId={createRequest.MerchantId}, httpStatus={responseMessage.Result.StatusCode}, httpContent={responseContent}]");
                _bus.PublishBusinessViolation("Payment Response Failed");
            }

            _logger.LogError($"Payment Request Failed [merchantId={createRequest.MerchantId}, httpStatus={responseMessage.Result.StatusCode}, httpContent={responseContent}]");
            _bus.PublishBusinessViolation("Payment Request Failed");

            return TryResult<CreatePaymentResult>.CreateFailResult();
        }
    }
}
