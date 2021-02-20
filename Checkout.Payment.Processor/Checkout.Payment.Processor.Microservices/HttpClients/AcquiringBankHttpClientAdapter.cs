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
using Checkout.Payment.Processor.Domain.Models.AcquiringBank;
using System.Collections.Specialized;

namespace Checkout.Payment.Processor.Domain.HttpClients
{
    public class AcquiringBankHttpClientAdapter : BaseHttpClientAdapter, IAcquiringBankHttpClientAdapter
    {
        public AcquiringBankHttpClientAdapter(HttpClient client, MicroServiceSettings microServiceSettings, ApplicationManifest manifest, ILogger<PaymentCommandHttpClientAdapter> logger) : base(client, logger)
        {
            _httpClient.BaseAddress = new Uri(microServiceSettings.AcquiringBankBaseAddress);
            _httpClient.DefaultRequestHeaders.Add("User-Agent", $"{manifest.Name} {manifest.Version}");
            _httpClient.DefaultRequestHeaders.Add("Authorization", microServiceSettings.AcquiringBankAuthorization);
        }

        public async Task<ITryResult<BankPaymentResponse>> TrySendPayment(BankPaymentRequest request)
		{
            var requestPayload = new
            {
                request.Amount,
                request.CardCVV,
                request.CardNumber,
                request.CurrencyType,
                request.ExpiryDate,
            };

            var responseResult = await TryPostJsonAsync($"{_httpClient.BaseAddress}v1/send", requestPayload);
            if (!responseResult.Success)
            {
                return TryResult<BankPaymentResponse>.CreateFailResult();
            }

            var responseContent = await responseResult.Result.Content.ReadAsStringAsync();
            if (responseResult.Result.IsSuccessStatusCode)
            {
                var updateResponse = JsonSerializer.Deserialize<BankPaymentResponse>(responseContent);

                _logger.LogInformation($"Bank Payment Call Succeeded [paymentId={request.PaymentId}, BankPaymentId={updateResponse.BankPaymentId}, PaymentStatus={updateResponse.PaymentStatus}]");
                return TryResult<BankPaymentResponse>.CreateSuccessResult(updateResponse);
            }

            _logger.LogError($"Bank Payment Call Failed [paymentId={request.PaymentId}, httpStatus={responseResult.Result.StatusCode}, httpContent={responseContent}]");

            return TryResult<BankPaymentResponse>.CreateFailResult();
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

            return TryResult<UpdatePaymentResponse>.CreateFailResult();
        }
    }
}
