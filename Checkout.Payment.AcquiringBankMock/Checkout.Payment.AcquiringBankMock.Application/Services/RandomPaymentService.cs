using Checkout.Payment.AcquiringBankMock.Application.Exceptions;
using Checkout.Payment.AcquiringBankMock.Application.Interfaces;
using Checkout.Payment.AcquiringBankMock.Application.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Checkout.Payment.AcquiringBankMock.Application.Services
{
	public class RandomPaymentService : IPaymentService
	{
		private KeyValuePair<string, string>[] _possibleStatuses = new [] {
			new KeyValuePair<string, string>("Succeeded", string.Empty),
			new KeyValuePair<string, string>("Rejected", "Incorrect"),
			new KeyValuePair<string, string>("Rejected", "Insufficient Funds"),
			new KeyValuePair<string, string>("Rejected", "Card Blocked"),
			new KeyValuePair<string, string>("Rejected", "Custom Error 101"),
		};

		private readonly ILogger<RandomPaymentService> _logger;

		public RandomPaymentService(ILogger<RandomPaymentService> logger)
		{
			_logger = logger;
		}
		private KeyValuePair<string, string> GetRandomStatus()
		{
			var randomKeyIndex = new Random().Next(_possibleStatuses.Length);

			return _possibleStatuses[randomKeyIndex];
		}

		public SendPaymentResponseModel ExecutePayment(SendPaymentRequestModel request)
		{
			var badRequest = new Random().Next(0, 10) % 2 == 1;

			if (badRequest)
			{
				_logger.LogDebug($"Failed PaymentResult - Odd");
				throw new BadRequestException("Something odd happened - Literaly");
			}

			var statusAndDetails = GetRandomStatus();

			var result = new SendPaymentResponseModel
			{
				RequestId = Guid.NewGuid(),
				Status = statusAndDetails.Key,
				StatusDetails = statusAndDetails.Value
			};

			_logger.LogDebug($"PaymentResult Generated [requestId={result.RequestId}, Status={result.Status}, StatusDetails={result.StatusDetails}]");
			return result;
		}
	}
}
