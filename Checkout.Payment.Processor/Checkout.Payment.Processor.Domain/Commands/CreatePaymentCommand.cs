﻿using Checkout.Payment.Processor.Domain.Models.Enums;
using Checkout.Payment.Processor.Seedwork.Extensions;
using MediatR;
using System;

namespace Checkout.Payment.Processor.Domain
{
    public class CreatePaymentCommand : IRequest<ITryResult<CreatePaymentCommandResponse>>
    {
        public int MerchantId { get; set; }
        public string CardNumber { get; set; }
        public int CardCVV { get; set; }
        public DateTime ExpiryDate { get; set; }
        public decimal Amount { get; set; }
        public CurrencyType CurrencyType { get; set; }
    }
}
