using Checkout.Payment.Processor.Domain.Models.AcquiringBank;
using Checkout.Payment.Processor.Domain.Models.Enums;

namespace Checkout.Payment.Processor.Domain.Commands
{
    public class SendBankPaymentCommandResponse
    {
		public SendBankPaymentCommandResponse(IBankPaymentResponse response)
		{
			BankPaymentId = response.BankPaymentId;
			PaymentStatusDetails = response.PaymentStatusDetails;
			PaymentStatus = response.PaymentStatus;
		}

		public string BankPaymentId { get; set; }
		public PaymentStatus PaymentStatus { get; set; }
        public string PaymentStatusDetails { get; set; }
    }
}
