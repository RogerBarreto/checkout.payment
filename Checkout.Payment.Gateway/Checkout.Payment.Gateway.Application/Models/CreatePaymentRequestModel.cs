using Checkout.Payment.Gateway.Application.Validations;
using System;
using System.ComponentModel.DataAnnotations;

namespace Checkout.Payment.Gateway.Application.Models
{
    public class CreatePaymentRequestModel
    {
        [CreditCard]
        public string CardNumber { get; set; }

        [Range(100, 999)]
        public int CardCVV { get; set; }

        [Required]
        public DateTime ExpiryDate { get; set; }

        [Range(0.01 , double.MaxValue)]
        public decimal Amount { get; set; }

        [CurrencyType]
        public string CurrencyType { get; set; }
    }
}
