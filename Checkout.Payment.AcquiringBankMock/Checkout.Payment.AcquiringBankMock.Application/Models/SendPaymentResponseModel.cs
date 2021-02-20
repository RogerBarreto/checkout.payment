using System;

namespace Checkout.Payment.AcquiringBankMock.Application.Models
{
	public class SendPaymentResponseModel
	{
		public Guid RequestId { get; set; }
		public string Status { get; set; }
		public string StatusDetails { get; set; }
	}
}
