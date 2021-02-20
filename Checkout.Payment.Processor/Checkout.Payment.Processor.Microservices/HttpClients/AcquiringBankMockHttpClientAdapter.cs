using System;
using System.Net.Http;
using System.Threading.Tasks;
using Checkout.Payment.Processor.Seedwork.Models;
using Microsoft.Extensions.Logging;
using Checkout.Payment.Processor.Seedwork.Extensions;
using System.Text.Json;
using Checkout.Payment.Processor.Domain.Interfaces;
using Checkout.Payment.Processor.Domain.Models.AcquiringBank;
using Checkout.Payment.Processor.MicroServices.Models;
using Checkout.Payment.Processor.MicroServices.Configurations;
using Checkout.Payment.Processor.Domain.Models.Enums;
using System.Net;

namespace Checkout.Payment.Processor.Domain.HttpClients
{
    public class AcquiringBankMockHttpClientAdapter : BaseHttpClientAdapter, IAcquiringBankHttpClientAdapter
    {
        public AcquiringBankMockHttpClientAdapter(HttpClient client, MicroServiceSettings microServiceSettings, ApplicationManifest manifest, ILogger<PaymentCommandHttpClientAdapter> logger) : base(client, logger)
        {
            _httpClient.BaseAddress = new Uri(microServiceSettings.AcquiringBankBaseAddress);
            _httpClient.DefaultRequestHeaders.Add("User-Agent", $"{manifest.Name} {manifest.Version}");
            _httpClient.DefaultRequestHeaders.Add("Authorization", microServiceSettings.AcquiringBankAuthorization);
        }

        public async Task<ITryResult<IBankPaymentResponse>> TrySendPayment(IBankPaymentRequest request)
		{
            var requestPayload = new
            {
                request.Amount,
                CVV = request.CardCVV,
                Number = request.CardNumber,
                Currency = request.CurrencyType.ToString(),
                ExpiryYear = request.ExpiryDate.Year,
                ExpiryMonth = request.ExpiryDate.Month
            };

            var responseResult = await TryPostJsonAsync($"{_httpClient.BaseAddress}payment/send", requestPayload);
            if (!responseResult.Success)
            {
                return TryResult<IBankPaymentResponse>.CreateFailResult();
            }

            var responseContent = await responseResult.Result.Content.ReadAsStringAsync();
            if (responseResult.Result.IsSuccessStatusCode)
            {
                var updateResponse = JsonSerializer.Deserialize<AcquiringBankMockPaymentResponse>(responseContent);

                _logger.LogInformation($"Bank Payment Call Succeeded [paymentId={request.PaymentId}, BankPaymentId={updateResponse.RequestId}, PaymentStatus={updateResponse.PaymentStatus}]");
                return TryResult<IBankPaymentResponse>.CreateSuccessResult(updateResponse);
            }

            if (responseResult.Result.StatusCode == HttpStatusCode.BadRequest)
            {
                var badRequestResponse = AcquiringBankMockBadRequestResponse.CreateFromJson(responseContent);

                var updateResponse = new AcquiringBankMockPaymentResponse
                {
                    Status = $"BadRequest - {badRequestResponse.Title}",
                    StatusDetails = badRequestResponse.AllErrors
                };

                _logger.LogInformation($"Bank Payment Call BadRequest [paymentId={request.PaymentId}, BankStatusDetails={updateResponse.StatusDetails}]");
                return TryResult<IBankPaymentResponse>.CreateSuccessResult(updateResponse);
            }

            _logger.LogError($"Bank Payment Call Failed [paymentId={request.PaymentId}, httpStatus={responseResult.Result.StatusCode}, httpContent={responseContent}]");

            return TryResult<IBankPaymentResponse>.CreateFailResult();
        }
    }
}
