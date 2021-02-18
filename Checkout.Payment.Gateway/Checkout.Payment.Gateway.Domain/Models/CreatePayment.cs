using Checkout.Payment.Gateway.Domain.Models.Enums;
using System;

namespace Checkout.Payment.Gateway.Domain.Models
{
    public class CreatePayment
    {
        public int MerchantId { get; set; }
        public string CardNumber { get; set; }
        public int CardCVV { get; set; }
        public DateTime ExpiryDate { get; set; }
        public decimal Amount { get; set; }
        public CurrencyType CurrencyType { get; set; }
    }
}
