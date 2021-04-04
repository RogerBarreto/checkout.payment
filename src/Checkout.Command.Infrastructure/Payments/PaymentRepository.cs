using System;
using System.Threading.Tasks;
using Checkout.Application.Common.Models.Payments;
using Checkout.Application.Common.Models.Payments.Commands;
using Checkout.Command.Application.Common.Interfaces;
using Checkout.Command.Application.Payments.Commands;
using Checkout.Domain.Entities;
using Checkout.Domain.Enums;
using Checkout.Domain.ValueObjects;
using Checkout.Infrastructure.Common.Extensions;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using OneOf;

namespace Checkout.Command.Infrastructure.Payments
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

		public async Task<OneOf<Guid, PaymentError>> CreatePaymentAsync(CreatePaymentCommand create)
		{
			var newPayment = new Payment
			{
				Id = Guid.NewGuid(),
				Amount = create.Amount,
				CardCVV = create.CardCVV,
				CardExpiry = new CardExpiry
				{
					Month  = create.CardExpiryMonth, 
					Year = create.CardExpiryYear
				},
				CardNumber = create.CardNumber,
				MerchantId = create.MerchantId,
				CurrencyType = Enum.Parse<CurrencyType>(create.CurrencyType),
				Status = PaymentStatus.Processing
			};

			string newPaymentData = null;
			try
			{
				newPaymentData = JsonSerializer.Serialize(newPayment);
				await _paymentCache.SetStringAsync($"Payment_{newPayment.Id}", newPaymentData);

				_logger.LogInformation($"Success to create payment in the Cache [paymentId={newPayment.Id}]");
				return newPayment.Id;
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to create a payment in cache [paymentData={newPaymentData}, message={ex.Message}]");
				return new PaymentError("Failed to create a payment");
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
		public async Task<OneOf<PaymentUpdated, PaymentNotFound, PaymentError>> UpdatePaymentAsync(UpdatePaymentCommand command)
		{
			try
			{
				var cachedStringValue = await _paymentCache.GetStringAsync($"Payment_{command.PaymentId}");
				if (cachedStringValue == null)
				{
					_logger.LogWarning($"Unable to update payment information - Cache key not found [paymentId={command.PaymentId}, updateData={JsonSerializer.Serialize(command)}]");
					return new PaymentNotFound("Payment not found");
				}

				var paymentRequest = JsonSerializer.Deserialize<Payment>(cachedStringValue);
				paymentRequest.UpdatePaymentStatus(command.PaymentStatus, command.PaymentStatusDescription, command.BankPaymentId);

				var updatedPaymentRequestData = JsonSerializer.Serialize(paymentRequest);
				await _paymentCache.SetStringAsync($"Payment_{command.PaymentId}", updatedPaymentRequestData);

				_logger.LogInformation($"Success to update update payment in the Cache [paymentId={command.PaymentId}]");
				return new PaymentUpdated();
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to update a payment in the Cache [paymentId={command.PaymentId}, updateData={JsonSerializer.Serialize(command)}, message={ex.Message}]");
				return new PaymentError("Failed to update a payment");
			}
		}

		public async Task<OneOf<PaymentDeleted, PaymentNotFound, PaymentError>> DeletePaymentAsync(Guid paymentId)
		{
			try
			{
				await _paymentCache.RemoveAsync($"Payment_{paymentId}");

				_logger.LogInformation($"Success to delete payment from Cache [paymentId={paymentId}]");
				return new PaymentDeleted();
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to delete a payment from Cache [paymentId={paymentId}, message={ex.Message}]");
				return new PaymentError("Failed to delete a payment from Cache");
			}
		}
	}
}
