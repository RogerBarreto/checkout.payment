using Checkout.Payment.Processor.Domain.Models.AcquiringBank;
using Checkout.Payment.Processor.Domain.Models.Enums;
using System;

namespace Checkout.Payment.Processor.Domain
{
    public class SendBankPaymentCommandResponse
    {
		public SendBankPaymentCommandResponse(BankPaymentResponse response)
		{
			BankPaymentId = response.BankPaymentId;
			PaymentStatusDetails = response.PaymentStatusDetails;
			PaymentStatus = ConvertPaymentStatusToEnum(response.PaymentStatus);
		}

		public string BankPaymentId { get; set; }
		public PaymentStatus PaymentStatus { get; set; }
        public string PaymentStatusDetails { get; set; }

		private PaymentStatus ConvertPaymentStatusToEnum(string bankStatus)
		{
			PaymentStatus result;
			if (!Enum.TryParse(bankStatus, out result))
			{
				result = PaymentStatus.Unexpected;
			};

			return result;
		}

    }
}
