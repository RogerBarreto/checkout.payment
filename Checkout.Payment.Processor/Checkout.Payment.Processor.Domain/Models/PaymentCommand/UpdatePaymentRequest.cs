using Checkout.Payment.Processor.Domain.Commands;
using Checkout.Payment.Processor.Domain.Models.Enums;
using System;

namespace Checkout.Payment.Processor.Domain.Models.PaymentCommand
{
	public class UpdatePaymentRequest
	{
		public Guid PaymentId { get; }
		public PaymentStatus PaymentStatus { get; }
		public string PaymentStatusDetails { get; }
		public string BankPaymentId { get; }

		public UpdatePaymentRequest(UpdatePaymentCommand command)
		{
			BankPaymentId = command.BankPaymentId;
			PaymentId = command.PaymentId;
			PaymentStatus = command.PaymentStatus;
			PaymentStatusDetails = command.PaymentStatusDetails;
		}
	}
}
