using Checkout.Domain.Entities;
using System;

namespace Checkout.Gateway.Application.Payments.Queries
{
	public class GetPaymentQueryResponse
	{
        public Guid PaymentId { get; set; }
        public string CardNumber { get; set; }
        public int CardExpiryMonth { get; set; }
        public int CardExpiryYear { get; set; }
        public decimal Amount { get; set; }
        public string CurrencyType { get; set; }
        public int PaymentStatusCode { get; set; }
        public string PaymentStatusDescription { get; set; }
		public string MaskedCardNumber => GetMaskedCardNumber();

        public string GetMaskedCardNumber(int showLastNthDigits = 4)
		{
			if (CardNumber is null)
			{
				return null;
			}

			var numberOfDigits = CardNumber.Length;

			if (numberOfDigits <= showLastNthDigits)
			{
				return MaskAllDigits(numberOfDigits);
			}

			return MaskPartiallyDigits(showLastNthDigits, numberOfDigits);
		}

		private string MaskPartiallyDigits(int showLastNthDigits, int numberOfDigits)
		{
			var maskedNumber = new string('*', numberOfDigits - showLastNthDigits);

			return $"{maskedNumber}{CardNumber.Substring(numberOfDigits - showLastNthDigits, showLastNthDigits)}";
		}

		private static string MaskAllDigits(int numberOfDigits)
		{
			return new string('*', numberOfDigits);
		}

		public static GetPaymentQueryResponse CreateFrom(Payment payment)
		{
            return new GetPaymentQueryResponse
            {
                Amount = payment.Amount,
                CardExpiryMonth = payment.CardExpiry.Month,
                CardExpiryYear = payment.CardExpiry.Year,
                CardNumber = payment.CardNumber,
                CurrencyType = payment.CurrencyType.ToString(),
                PaymentId = payment.Id,
                PaymentStatusCode = (int)payment.Status,
                PaymentStatusDescription = payment.StatusDescription
            };
        }
    }
}
