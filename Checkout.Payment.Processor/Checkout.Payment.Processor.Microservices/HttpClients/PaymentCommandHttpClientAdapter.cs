using System;
using System.Net.Http;
using System.Threading.Tasks;
using Checkout.Payment.Processor.Seedwork.Models;
using Microsoft.Extensions.Logging;
using Checkout.Payment.Processor.Seedwork.Extensions;
using Checkout.Payment.Processor.Domain.Models.PaymentCommand;
using System.Text.Json;
using Checkout.Payment.Processor.Domain.Interfaces;
using Checkout.Payment.Processor.MicroServices.Configurations;
using System.Net;

namespace Checkout.Payment.Processor.Domain.HttpClients
{
    public class PaymentCommandHttpClientAdapter : BaseHttpClientAdapter, IPaymentCommandHttpClientAdapter
    {
        public PaymentCommandHttpClientAdapter(HttpClient client, MicroServiceSettings microServiceSettings, ApplicationManifest manifest, ILogger<PaymentCommandHttpClientAdapter> logger) : base(client, logger)
        {
            _httpClient.BaseAddress = new Uri(microServiceSettings.PaymentCommandBaseAddress);
            _httpClient.DefaultRequestHeaders.Add("User-Agent", $"{manifest.Name} {manifest.Version}");
        }

		public async Task<ITryResult<UpdatePaymentResponse>> TryUpdatePayment(UpdatePaymentRequest updateRequest)
		{
            var requestPayload = new
            {
                updateRequest.BankPaymentId,
                updateRequest.PaymentStatus,
                updateRequest.PaymentStatusDetails
            };

            var responseMessage = await TryPutJsonAsync($"{_httpClient.BaseAddress}payment/{updateRequest.PaymentId}", requestPayload);
            if (!responseMessage.Success)
            {
                return TryResult<UpdatePaymentResponse>.CreateFailResult();
            }

            var responseContent = await responseMessage.Result.Content.ReadAsStringAsync();
            if (responseMessage.Result.IsSuccessStatusCode) 
            {
                if (responseMessage.Result.StatusCode == HttpStatusCode.OK)
                {
                    var updateResponse = JsonSerializer.Deserialize<UpdatePaymentResponse>(responseContent);

                    _logger.LogInformation($"Update Payment Request Succeeded [PaymentId={updateRequest.PaymentId}, PaymentStatus={updateRequest.PaymentStatus}]");
                    return TryResult<UpdatePaymentResponse>.CreateSuccessResult(updateResponse);
                }
                else
				{
                    _logger.LogError($"Update Payment Failed - Not Found / No Content [PaymentId={updateRequest.PaymentId}, PaymentRequest={requestPayload}]");
                    return TryResult<UpdatePaymentResponse>.CreateFailResult("notFound");
                }
            }

            _logger.LogError($"Update Payment Failed [paymentId={updateRequest.PaymentId}, httpStatus={responseMessage.Result.StatusCode}, httpContent={responseContent}]");
            return TryResult<UpdatePaymentResponse>.CreateFailResult();
        }
    }
}
