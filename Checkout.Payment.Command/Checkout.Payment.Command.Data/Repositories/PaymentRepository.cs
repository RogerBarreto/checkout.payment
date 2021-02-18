using Checkout.Payment.Command.Domain;
using Checkout.Payment.Command.Domain.Interfaces;
using Checkout.Payment.Command.Domain.Models;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Threading.Tasks;
using System.Text.Json;
using Checkout.Payment.Command.Seedwork.Extensions;
using Microsoft.Extensions.Logging;

namespace Checkout.Payment.Command.Data
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

        public async Task<ITryResult<PaymentRequest>> TryCreatePayment(CreatePaymentCommand request)
        {
            var newPayment = new PaymentRequest(Guid.NewGuid(), request);
            string newPaymentData = null;
            try
            {
                newPaymentData = JsonSerializer.Serialize(newPayment);
                await _paymentCache.SetStringAsync($"Payment_{newPayment.PaymentId}", newPaymentData);

                return TryResult<PaymentRequest>.CreateSuccessResult(newPayment);
			}
            catch(Exception ex)
			{
                _logger.LogError($"Failed to create a payment in cache [paymentData={newPaymentData}, message={ex.Message}]");
                return TryResult<PaymentRequest>.CreateFailResult();
            }
        }

		public async Task<ITryResult> TryRemovePayment(Guid paymentId)
		{
            try
            {
                await _paymentCache.RemoveAsync($"Payment_{paymentId}");

                return TryResult.CreateSuccessResult();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to remove a payment from Cache [paymentId={paymentId}, message={ex.Message}]");
                return TryResult.CreateFailResult();
            }
        }
	}
}
