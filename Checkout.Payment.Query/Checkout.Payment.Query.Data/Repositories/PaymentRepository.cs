using Checkout.Payment.Query.Domain;
using Checkout.Payment.Query.Domain.Interfaces;
using Checkout.Payment.Query.Domain.Models;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Threading.Tasks;
using System.Text.Json;
using Checkout.Payment.Query.Seedwork.Extensions;
using Microsoft.Extensions.Logging;

namespace Checkout.Payment.Query.Data
{
    public class PaymentRepository : IPaymentRepository
    {
        IDistributedCache _paymentCache;
		private readonly ILogger<PaymentRepository> _logger;

		public PaymentRepository(IDistributedCache paymentCache, ILogger<PaymentRepository> logger)
        {
            _paymentCache = paymentCache;
			_logger = logger;
		}

        public async Task<ITryResult<PaymentRequest>> TryGetPayment(Guid paymentId)
        {
            try
            {
                var paymentData = await _paymentCache.GetStringAsync($"Payment_{paymentId}");

                if (paymentData != null)
                {
                    var paymentRequest = JsonSerializer.Deserialize<PaymentRequest>(paymentData);

                    _logger.LogInformation($"Success getting payment from cache [paymentId={paymentId}]");
                    return TryResult<PaymentRequest>.CreateSuccessResult(paymentRequest);
                }
                else
				{
                    _logger.LogInformation($"Not found payment from cache [paymentId={paymentId}]");
                    return TryResult<PaymentRequest>.CreateFailResult();
                }
            }
            catch(Exception ex)
			{
                _logger.LogError($"Failed to get payment from cache [paymentId={paymentId}, message={ex.Message}]");
                return TryResult<PaymentRequest>.CreateFailResult();
            }
        }
	}
}
