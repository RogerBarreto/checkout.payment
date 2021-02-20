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

                _logger.LogInformation($"Success to create payment in the Cache [paymentId={newPayment.PaymentId}]");
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

                _logger.LogInformation($"Success to remove payment from Cache [paymentId={paymentId}]");
                return TryResult.CreateSuccessResult();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to remove a payment from Cache [paymentId={paymentId}, message={ex.Message}]");
                return TryResult.CreateFailResult();
            }
        }

        /// <summary>
        /// Try to updates the cache with the given payload
        /// </summary>
        /// <param name="command"></param>
        /// <returns>
        /// Returns Success.true if cache is found and updated
        /// Returns Success.false if the cache was not found (nothing was updated)</returns>
        /// Returns Failure.false if unexpected error happened</returns>
		public async Task<ITryResult<bool>> TryUpdatePayment(UpdatePaymentCommand command)
		{
            try
            {
                var cachedStringValue = await _paymentCache.GetStringAsync($"Payment_{command.PaymentId}");
                if (cachedStringValue == null)
				{
                    _logger.LogWarning($"Unable to update payment information - Cache key not found [paymentId={command.PaymentId}, updateData={JsonSerializer.Serialize(command)}]");
                    return TryResult<bool>.CreateSuccessResult(false);
                }

                var paymentRequest = JsonSerializer.Deserialize<PaymentRequest>(cachedStringValue);
                paymentRequest.UpdatePayment(command);

                var updatedPaymentRequestData = JsonSerializer.Serialize(paymentRequest);
                await _paymentCache.SetStringAsync($"Payment_{command.PaymentId}", updatedPaymentRequestData);

                _logger.LogInformation($"Success to update update payment in the Cache [paymentId={command.PaymentId}]");
                return TryResult<bool>.CreateSuccessResult(true);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to update a payment in the Cache [paymentId={command.PaymentId}, updateData={JsonSerializer.Serialize(command)}, message={ex.Message}]");
                return TryResult<bool>.CreateFailResult();
            }
        }
	}
}
