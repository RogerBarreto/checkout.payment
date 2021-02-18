using Checkout.Payment.Processor.Domain;
using Checkout.Payment.Processor.Domain.Interfaces;
using Checkout.Payment.Processor.Domain.Models;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Threading.Tasks;
using System.Text.Json;
using Checkout.Payment.Processor.Seedwork.Extensions;
using Microsoft.Extensions.Logging;

namespace Checkout.Payment.Processor.Data
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

        public async Task<ITryResult<PaymentMessage>> TryCreatePayment(CreatePaymentCommand request)
        {
            var newPayment = new PaymentMessage(Guid.NewGuid(), request);
            string newPaymentData = null;
            try
            {
                newPaymentData = JsonSerializer.Serialize(newPayment);
                await _paymentCache.SetStringAsync($"Payment_{newPayment.PaymentId}", newPaymentData);

                return TryResult<PaymentMessage>.CreateSuccessResult(newPayment);
			}
            catch(Exception ex)
			{
                _logger.LogError($"Failed to create a payment in cache [paymentData={newPaymentData}, message={ex.Message}]");
                return TryResult<PaymentMessage>.CreateFailResult();
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
