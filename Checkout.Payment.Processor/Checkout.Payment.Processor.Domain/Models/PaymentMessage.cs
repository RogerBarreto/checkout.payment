using Checkout.Payment.Processor.Domain.Models.Enums;
using System;

namespace Checkout.Payment.Processor.Domain.Models
{
    public class PaymentMessage
    {
        public Guid PaymentId { get; set; }
        public string CardNumber { get; set; }
        public int CardCVV { get; set; }
        public decimal Amount { get; set; }
        public CurrencyType CurrencyType { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public string BankPaymentId { get; set; }
    }
}
