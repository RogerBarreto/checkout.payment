using Checkout.Domain.Entities;
using Checkout.Domain.Enums;
using Checkout.Domain.ValueObjects;
using MediatR;
using System;

namespace Checkout.Gateway.Application.Payments.Commands
{
	public class CreatePaymentCommand : IRequest<CreatePaymentCommandResponse>
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
