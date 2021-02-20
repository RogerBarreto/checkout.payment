using Checkout.Payment.Processor.Domain.Models.Enums;
using Checkout.Payment.Processor.Seedwork.Extensions;
using MediatR;
using System;

namespace Checkout.Payment.Processor.Domain
{
    public class UpdatePaymentCommand : IRequest<ITryResult>
    {
        public Guid PaymentId { get; set; }
        public string BankPaymentId { get; set; }
		public PaymentStatus PaymentStatus { get; set; }
		public string PaymentStatusDetails { get; set; }
	}
}
