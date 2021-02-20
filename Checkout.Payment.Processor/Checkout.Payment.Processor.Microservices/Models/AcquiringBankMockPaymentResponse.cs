using Checkout.Payment.Processor.Domain.Models.AcquiringBank;
using Checkout.Payment.Processor.Domain.Models.Enums;

namespace Checkout.Payment.Processor.MicroServices.Models
{
	public class AcquiringBankMockPaymentResponse : IBankPaymentResponse
	{
		public string RequestId { get; set; }
		public string Status { get; set; }
		public string StatusDetails { get; set; }
		public string BankPaymentId => RequestId;
		public PaymentStatus PaymentStatus => GetPaymentStatus();
		public string PaymentStatusDetails => StatusDetails;

		private PaymentStatus GetPaymentStatus()
		{
			var paymentStatusAndDetails = $"{Status}-{StatusDetails}";
			switch (paymentStatusAndDetails)
			{
				case "Succeeded-": return PaymentStatus.Succeeded;
				case "Rejected-Incorrect": return PaymentStatus.RejectedIncorrect;
				case "Rejected-Insufficient Funds": return PaymentStatus.RejectedInsuficientFunds;
				case "Rejected-Card Blocked": return PaymentStatus.RejectedCardBlocked;
				default: return PaymentStatus.RejectedCustom;
			}
		}
	}



}
