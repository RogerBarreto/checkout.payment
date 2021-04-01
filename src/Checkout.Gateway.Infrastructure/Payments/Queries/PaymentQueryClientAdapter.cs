using Checkout.Gateway.Application.Common.Interfaces;
using Checkout.Infrastructure.Common.HttpClients;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Specialized;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Checkout.Domain.Errors;
using Checkout.Gateway.Application.Payments.Queries;
using Checkout.Infrastructure.Common.HttpClients.Errors;
using OneOf;

namespace Checkout.Gateway.Infrastructure.Payments.Queries
{
	public class PaymentQueryClientAdapter : BaseHttpClientAdapter, IPaymentQueryClient
	{
		public PaymentQueryClientAdapter(HttpClient httpClient, IConfiguration configuration, ILogger<PaymentQueryClientAdapter> logger) : 
            base(httpClient, logger)
		{
			_httpClient.BaseAddress = new Uri(configuration.GetSection("HttpClientSettings:PaymentQuery:BaseAddress").Value);
		}

		public async Task<OneOf<GetPaymentQueryResponse, PaymentNotFound, PaymentError>> GetPaymentAsync(GetPaymentQuery request)
		{
            var responseMessageResult = await GetAsync($"{_httpClient.BaseAddress}payment/{request.PaymentId}", new NameValueCollection { { "MerchantId", request.MerchantId.ToString() } });
            if (responseMessageResult.Value is HttpCallError error)
            {
	            return new PaymentError(error.Message);
            }

            var responseMessage = responseMessageResult.Value as HttpResponseMessage;
            var responseContent = await responseMessage.Content.ReadAsStringAsync();
            if (responseMessage.IsSuccessStatusCode)
            {
                var paymentResponse = JsonSerializer.Deserialize<GetPaymentQueryResponse>(responseContent);
                _logger.LogInformation($"Payment Found [paymentId={paymentResponse.PaymentId}]");
                return paymentResponse;
            }

            if (responseMessage.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                _logger.LogInformation($"Payment Not Found [paymentId={request.PaymentId}]");
                return new PaymentNotFound("Payment Not Found");
                //_bus.PublishNotFound("Payment Not Found");
            }

            _logger.LogError($"Payment Query Failed [paymentId={request.PaymentId}, merchantId={request.MerchantId}, httpStatus={responseMessage.StatusCode}, httpContent={responseContent}]");
            //_bus.PublishBusinessViolation("Failed to get Payment Details");
            
            return new PaymentError("Payment Query Failed");
        }
	}
}
