using Checkout.Payment.Query.Domain.Models.Enums;
using System;

namespace Checkout.Payment.Query.Domain.Models
{
    public class PaymentRequest
    {
        public int MerchantId { get; set; }
        public Guid PaymentId { get; set; }
        public string CardNumber { get; set; }
        public int CardCVV { get; set; }
        public decimal Amount { get; set; }
        public DateTime ExpiryDate { get; set; }
        public CurrencyType CurrencyType { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public string PaymentStatusDetails { get; set; }
        public string BankPaymentId { get; set; }
    }
}
