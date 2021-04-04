﻿using MediatR;
using OneOf;

namespace Checkout.Application.Common.Models.Payments.Commands
{
	public class CreatePaymentCommand : IRequest<OneOf<CreatePaymentCommandResponse, PaymentError>>
	{
        public int MerchantId { get; set; }
        public string CardNumber { get; set; }
        public int CardCVV { get; set; }
        public int CardExpiryYear { get; set; }
        public int CardExpiryMonth { get; set; }
        public decimal Amount { get; set; }
        public string CurrencyType { get; set; }
    }
}