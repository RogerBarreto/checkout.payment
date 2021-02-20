using Checkout.Payment.Command.Domain.Models.Enums;
using System;

namespace Checkout.Payment.Command.Domain.Models
{
    public class PaymentRequest
    {
        public Guid PaymentId { get; set; }
        public string CardNumber { get; set; }
        public int CardCVV { get; set; }
        public decimal Amount { get; set; }
        public DateTime ExpiryDate { get; set; }
        public CurrencyType CurrencyType { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public string PaymentStatusDetails { get; set; }
        public string BankPaymentId { get; set; }

        public PaymentRequest() { }
        public PaymentRequest(Guid paymentId, CreatePaymentCommand createPaymentRequest)
        {
            PaymentId = paymentId;
            CardNumber = createPaymentRequest.CardNumber;
            CardCVV = createPaymentRequest.CardCVV;
            Amount = createPaymentRequest.Amount;
            CurrencyType = createPaymentRequest.CurrencyType;
            ExpiryDate = createPaymentRequest.ExpiryDate;
            PaymentStatus = PaymentStatus.Processing;
        }
        public void UpdatePayment(UpdatePaymentCommand command)
        {
            if (PaymentId != command.PaymentId)
			{
                throw new ArgumentException($"Cannot update a paymentRequest from a different paymentId [currentPaymentId={PaymentId}, updatePaymentId={command.PaymentId}]");
			}

            BankPaymentId = command.BankPaymentId;
            PaymentStatusDetails = command.PaymentStatusDetails;
            PaymentStatus = command.PaymentStatus;
        }
    }
}
