using Checkout.Payment.Command.Domain.Models.Enums;
using Checkout.Payment.Command.Seedwork.Extensions;
using MediatR;
using System;

namespace Checkout.Payment.Command.Domain
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
