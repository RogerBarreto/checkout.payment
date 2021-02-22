using Checkout.Payment.Query.Domain.Models;
using Checkout.Payment.Query.Domain.Models.Enums;
using System;

namespace Checkout.Payment.Query.Domain
{
    public class GetPaymentQueryResponse
    {
        public Guid PaymentId { get; set; }
        public string CardNumber { get; set; }
        public DateTime ExpiryDate { get; set; }
        public decimal Amount { get; set; }
        public CurrencyType CurrencyType { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public string PaymentStatusDetails { get; set; }

		public GetPaymentQueryResponse(PaymentRequest request)
		{
            PaymentId = request.PaymentId;
            CardNumber = request.CardNumber;
            ExpiryDate = request.ExpiryDate;
            Amount = request.Amount;
            CurrencyType = request.CurrencyType;
            PaymentStatus = request.PaymentStatus;
            PaymentStatusDetails = request.PaymentStatusDetails;
		}
    }
}
