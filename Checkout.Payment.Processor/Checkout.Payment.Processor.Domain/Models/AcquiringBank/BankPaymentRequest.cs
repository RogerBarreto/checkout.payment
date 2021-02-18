using Checkout.Payment.Processor.Domain.Models.Enums;
using System;

namespace Checkout.Payment.Processor.MicroServices.Models.AcquiringBank
{
	public class BankPaymentRequest
	{
        public string CardNumber { get; set; }
        public int CardCVV { get; set; }
        public DateTime ExpiryDate { get; set; }
        public decimal Amount { get; set; }
        public CurrencyType CurrencyType { get; set; }
    }
}
