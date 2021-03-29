using Checkout.Gateway.Application.Common.Interfaces;
using Checkout.Gateway.Application.Payments.Commands;
using Checkout.Gateway.Application.Payments.Errors;
using Checkout.Infrastructure.Common.HttpClients;
using Checkout.Infrastructure.Common.HttpClients.Errors;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OneOf;
using System;
using System.Collections.Specialized;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Checkout.Gateway.Infrastructure.Payments.Commands
{
	public class PaymentCommandClientAdapter : BaseHttpClientAdapter, IPaymentCommandClient
	{
		public PaymentCommandClientAdapter(HttpClient httpClient, IConfiguration configuration, ILogger<PaymentCommandClientAdapter> logger) : 
            base(httpClient, logger)
		{
			_httpClient.BaseAddress = new Uri(configuration.GetSection("HttpClientSettings:PaymentCommand:BaseAddress").Value);
		}

		public async Task<OneOf<Guid, PaymentError>> CreatePaymentAsync(CreatePaymentCommand createRequest)
		{
            var requestPayload = new
            {
                createRequest.Amount,
                createRequest.CardNumber,
                createRequest.CardCVV,
                createRequest.CardExpiryYear,
                createRequest.CardExpiryMonth,
                createRequest.CurrencyType
            };

            var responseMessageResult = await PostJsonAsync($"{_httpClient.BaseAddress}payment", requestPayload, new NameValueCollection { { "MerchantId", createRequest.MerchantId.ToString() } });


            if (responseMessageResult.Value is HttpCallError error)
            {
                //_bus.PublishError(error.Message);
                return new PaymentError(error.Message);
            }
            var responseMessage = responseMessageResult.Value as HttpResponseMessage;

            var responseContent = await responseMessage.Content.ReadAsStringAsync();
            if (responseMessage.IsSuccessStatusCode)
            {
                var paymentResponse = JsonSerializer.Deserialize<CreatePaymentDto>(responseContent);
                if (paymentResponse != null)
                {
                    _logger.LogInformation($"Payment Request Succeeded [PaymentId={paymentResponse.PaymentId}]");
                    return paymentResponse.PaymentId;
                }

                _logger.LogError($"Payment Response Failed [merchantId={createRequest.MerchantId}, httpStatus={responseMessage.StatusCode}, httpContent={responseContent}]");
                //_bus.PublishBusinessViolation("Payment Response Failed");
                return new PaymentError("Payment Response Failed");
            }

            _logger.LogError($"Payment Request Failed [merchantId={createRequest.MerchantId}, httpStatus={responseMessage.StatusCode}, httpContent={responseContent}]");
            //_bus.PublishBusinessViolation("Payment Request Failed");

            return new PaymentError("Payment Request Failed");
        }
	}
}
