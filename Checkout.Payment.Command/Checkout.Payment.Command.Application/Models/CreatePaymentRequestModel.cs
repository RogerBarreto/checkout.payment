﻿using Checkout.Payment.Command.Application.Models.Enums;
using Checkout.Payment.Command.Application.Validations;
using System;
using System.ComponentModel.DataAnnotations;

namespace Checkout.Payment.Command.Application.Models
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
        public CurrencyTypeModel CurrencyType { get; set; }
    }
}
