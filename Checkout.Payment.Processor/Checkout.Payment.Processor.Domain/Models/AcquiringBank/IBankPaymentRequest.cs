using System;

namespace Checkout.Payment.Processor.Domain.Models.AcquiringBank
{
	public interface IBankPaymentRequest
	{
		public Guid PaymentId { get; set; }
		public string CardNumber { get; set; }
        public int CardCVV { get; set; }
        public DateTime ExpiryDate { get; set; }
        public decimal Amount { get; set; }
        public string CurrencyType { get; set; }
	}
}
