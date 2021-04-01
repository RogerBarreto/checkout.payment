using Checkout.Domain.Entities;
using System;

namespace Checkout.Query.Application.Payments.Queries
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
