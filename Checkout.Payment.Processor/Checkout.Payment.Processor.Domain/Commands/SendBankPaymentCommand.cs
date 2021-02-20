using Checkout.Payment.Processor.Domain.Models.Enums;
using Checkout.Payment.Processor.Seedwork.Extensions;
using MediatR;
using System;

namespace Checkout.Payment.Processor.Domain
{
    public class SendBankPaymentCommand : IRequest<ITryResult<SendBankPaymentCommandResponse>>
    {
        public string CardNumber { get; set; }
        public int CardCVV { get; set; }
        public DateTime ExpiryDate { get; set; }
        public decimal Amount { get; set; }
        public CurrencyType CurrencyType { get; set; }
        public string BankPaymentId { get; set; }
		public Guid PaymentId { get; set; }
	}
}
