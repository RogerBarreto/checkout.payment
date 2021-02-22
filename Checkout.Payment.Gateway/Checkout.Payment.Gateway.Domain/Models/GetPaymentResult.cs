using Checkout.Payment.Gateway.Domain.Models.Enums;
using System;

namespace Checkout.Payment.Gateway.Domain.Models
{
    public class GetPaymentResult
    {
        public Guid PaymentId { get; set; }
        public string CardNumber { get; set; }
        public int CardExpiryMonth { get; set; }
        public int CardExpiryYear { get; set; }
        public decimal Amount { get; set; }
        public CurrencyType CurrencyType { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public string PaymentStatusDetails { get; set; }
    }
}
