using Checkout.Payment.Processor.Domain.Commands;
using Checkout.Payment.Processor.Domain.Models.Enums;
using System;

namespace Checkout.Payment.Processor.Domain.Models.AcquiringBank
{
	public interface IBankPaymentResponse
	{
		string BankPaymentId { get; }
		PaymentStatus PaymentStatus { get; }
		string PaymentStatusDetails { get; }
	}
}
