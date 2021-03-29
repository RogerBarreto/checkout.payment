using Checkout.Domain.Entities;
using Checkout.Gateway.Application.Common.Interfaces;
using Checkout.Infrastructure.Common.HttpClients;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Checkout.Gateway.Infrastructure.Payments.Queries
{
	public class PaymentQueryClient : BaseHttpClientAdapter, IPaymentQueryClient
	{
		private readonly IConfiguration _configuration;

		public PaymentQueryClient(HttpClient httpClient, IConfiguration configuration, ILogger<PaymentQueryClient> logger) : 
            base(httpClient, logger)
		{
			_configuration = configuration;
			_httpClient.BaseAddress = new Uri(_configuration.GetSection("HttpClientSettings:PaymentQuery:BaseAddress").Value);
		}

		public Task<Payment> GetPaymentAsync(Guid paymentId)
		{
            var responseMessageResult = await GetAsync($"{_httpClient.BaseAddress}payment/{request.PaymentId}", new NameValueCollection { { "MerchantId", request.MerchantId.ToString() } });


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
