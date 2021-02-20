using System;
using System.Collections.Generic;
using System.Text;

namespace Checkout.Payment.Processor.Domain.Models.AcquiringBank
{
	public class BankPaymentResponse
	{
		public Guid RequestId { get; set; }
		public string PaymentStatus { get; set; }
		public string PaymentStatusDetails { get; set; }
		public string BankPaymentId { get; internal set; }
	}
}
