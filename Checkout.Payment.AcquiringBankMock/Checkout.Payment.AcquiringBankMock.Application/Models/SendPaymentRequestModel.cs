using Checkout.Payment.AcquiringBankMock.Application.Validations;
using System.ComponentModel.DataAnnotations;

namespace Checkout.Payment.AcquiringBankMock.Application.Models
{
	public class SendPaymentRequestModel
	{
		[CreditCard]
		public string Number { get; set; }
		[Range(100, 999)]
		public int CVV { get; set; }
		
		[Range(1, 12)]
		public int ExpiryMonth { get; set; }
		
		[ExpiryYear]
		public int ExpiryYear { get; set; }

		[Range(1, double.MaxValue)]
		public decimal Amount { get; set; }

		[Currency]
		public string Currency { get; set; }
	}
}
