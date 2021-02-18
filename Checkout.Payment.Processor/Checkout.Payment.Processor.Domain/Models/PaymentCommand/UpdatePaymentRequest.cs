using System;
using System.Collections.Generic;
using System.Text;

namespace Checkout.Payment.Processor.Domain.Models.PaymentCommand
{
	public class UpdatePaymentRequest
	{
		public object PaymentId { get; set; }
		public object PaymentStatus { get; set; }
		public object PaymentStatusDetails { get; set; }
	}
}
