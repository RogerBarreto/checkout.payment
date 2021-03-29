using Checkout.Domain.Enums;
using Checkout.Domain.ValueObjects;
using System;

namespace Checkout.Domain.Entities
{
	public class Payment
	{
        public Guid Id { get; set; }
        public int MerchantId { get; set; }
        public string CardNumber { get; set; }
        public int CardCVV { get; set; }
        public decimal Amount { get; set; }
        public CardExpiry CardExpiry { get; set; }
        public CurrencyType CurrencyType { get; set; }
        public PaymentStatus Status { get; set; }
        public string StatusDescription { get; set; }
        public string BankPaymentId { get; set; }
    }
}
