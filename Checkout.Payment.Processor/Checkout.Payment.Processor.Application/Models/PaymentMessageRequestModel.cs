using Checkout.Payment.Processor.Application.Models.Enums;
using System;

namespace Checkout.Payment.Processor.Application.Models
{
    public class PaymentMessageRequestModel
    {
        public Guid PaymentId { get; set; }
        public string CardNumber { get; set; }

        public int CardCVV { get; set; }

        public DateTime ExpiryDate { get; set; }

        public decimal Amount { get; set; }

        public string CurrencyType { get; set; }

        public string BankPaymentId { get; set; }

        public PaymentStatusModel PaymentStatus { get; set; }
		public string PaymentStatusDetails { get; set; }
	}
}
