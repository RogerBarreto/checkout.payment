using System;
using System.Text.Json;
using System.Threading.Tasks;
using Checkout.Application.Common.Payments.Queries;
using Checkout.Domain.Entities;
using Checkout.Domain.Errors;
using Checkout.Query.Application.Common.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using OneOf;

namespace Checkout.Query.Infrastructure.Payments
{
	internal class PaymentRepository : IPaymentRepository
	{
		readonly IDistributedCache _paymentCache;

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

				if (paymentData != null)
				{
					var payment = JsonSerializer.Deserialize<Payment>(paymentData);

					_logger.LogInformation($"Success getting payment from cache [paymentId={payment.Id}]");
					return payment;
				}
				else
				{
					_logger.LogInformation($"Payment not found [paymentId={query.PaymentId}]");
					return new PaymentNotFound("Payment not found");
				}
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to get payment from cache [paymentId={query.PaymentId}, message={ex.Message}]");
				return new PaymentError("Failed to get payment from cache");
			}
		}
	}
}
