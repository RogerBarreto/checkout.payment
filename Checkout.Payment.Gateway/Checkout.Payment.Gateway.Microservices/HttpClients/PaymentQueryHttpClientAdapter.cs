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
    public class PaymentQueryHttpClientAdapter : BaseHttpClientAdapter, IPaymentQueryHttpClientAdapter
    {
        private readonly IDomainNotificationBus _bus;

        public PaymentQueryHttpClientAdapter(HttpClient client, MicroServiceSettings microServiceSettings, ApplicationManifest manifest, IDomainNotificationBus notificationBus, ILogger<PaymentQueryHttpClientAdapter> logger) : base(client, logger)
        {
            _bus = notificationBus;
            _httpClient.BaseAddress = new Uri(microServiceSettings.PaymentQueryBaseAddress);
            _httpClient.DefaultRequestHeaders.Add("User-Agent", $"{manifest.Name} {manifest.Version}");
        }

        public async Task<ITryResult<GetPaymentResult>> TryGetPayment(GetPayment request)
        {
            var responseMessage = await TryGetAsync($"{_httpClient.BaseAddress}payment/{request.PaymentId}", new NameValueCollection { { "MerchantId", request.MerchantId.ToString() } });

            if (!responseMessage.Success)
            {
                _bus.PublishError(responseMessage.Message);
                return TryResult<GetPaymentResult>.CreateFailResult();
            }

            var responseContent = await responseMessage.Result.Content.ReadAsStringAsync();
            if (responseMessage.Result.IsSuccessStatusCode)
            {
                var paymentResponse = JsonConvert.DeserializeObject<GetPaymentResult>(responseContent);

                _logger.LogInformation($"Payment Found [paymentId={paymentResponse.PaymentId}]");
                return TryResult<GetPaymentResult>.CreateSuccessResult(paymentResponse);
            }

            if (responseMessage.Result.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                _logger.LogInformation($"Payment Not Found [paymentId={request.PaymentId}]");
                _bus.PublishNotFound("Payment Not Found");
            }
            else
            {
                _logger.LogError($"Payment Query Failed [paymentId={request.PaymentId}, merchantId={request.MerchantId}, httpStatus={responseMessage.Result.StatusCode}, httpContent={responseContent}]");
                _bus.PublishBusinessViolation("Failed to get Payment Details");
            }
            return TryResult<GetPaymentResult>.CreateFailResult();
        }
    }
}
