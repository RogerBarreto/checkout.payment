using Checkout.Payment.Processor.Domain.Models.Enums;
using Checkout.Payment.Processor.Seedwork.Extensions;
using MediatR;
using System;

namespace Checkout.Payment.Processor.Domain.Commands
{
	public class ReprocessPaymentCommand : IRequest<ITryResult>
	{
        public Guid PaymentId { get; set; }
        public string CardNumber { get; set; }
        public int CardCVV { get; set; }
        public DateTime ExpiryDate { get; set; }
        public decimal Amount { get; set; }
        public CurrencyType CurrencyType { get; set; }
        public string BankPaymentId { get; set; }
		public PaymentStatus PaymentStatus { get; set; }
		public string PaymentStatusDetails { get; set; }
	}
}
