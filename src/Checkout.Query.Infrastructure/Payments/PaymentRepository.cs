using System;
using System.Threading.Tasks;
using Checkout.Application.Common.Models.Payments;
using Checkout.Application.Common.Models.Payments.Queries;
using Checkout.Domain.Entities;
using Checkout.Infrastructure.Common.Extensions;
using Checkout.Query.Application.Common.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using OneOf;

namespace Checkout.Query.Infrastructure.Payments
{
	internal class PaymentRepository : IPaymentRepository
	{
		private readonly IDistributedCache _paymentCache;

		private readonly ILogger<PaymentRepository> _logger;

		public PaymentRepository(IDistributedCache paymentCache, ILogger<PaymentRepository> logger)
		{
			_paymentCache = paymentCache;
			_logger = logger;
		}

		public async Task<OneOf<Payment, PaymentNotFound, PaymentError>> GetPaymentAsync(GetPaymentQuery query)
		{
			try
			{
				var paymentData = await _paymentCache.GetStringAsync($"Payment_{query.PaymentId}");

				if (paymentData == null)
				{
					_logger.LogInformation($"Payment not found [paymentId={query.PaymentId}]");
					return new PaymentNotFound("Payment not found");
				}

				var payment = JsonSerializer.Deserialize<Payment>(paymentData);
				if (payment.MerchantId != query.MerchantId)
				{
					_logger.LogWarning($"Payment not found - Exists in cache for different merchant id [paymentId={payment.Id}, merchantId={query.MerchantId}, cachedMerchantId={payment.MerchantId}]");
					return new PaymentNotFound("Payment not found");
				}

				_logger.LogInformation($"Success getting payment from cache [paymentId={payment.Id}]");
				return payment;
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to get payment from cache [paymentId={query.PaymentId}, message={ex.Message}]");
				return new PaymentError("Failed to get payment from cache");
			}
		}
	}
}
