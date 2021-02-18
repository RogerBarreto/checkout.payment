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
        public CurrencyType CurrencyType { get; set; }
        public PaymentStatus PaymentStatus { get; set; }

        public PaymentRequest(Guid paymentId, CreatePaymentCommand createPaymentRequest)
        {
            PaymentId = paymentId;
            CardNumber = createPaymentRequest.CardNumber;
            CardCVV = createPaymentRequest.CardCVV;
            Amount = createPaymentRequest.Amount;
            CurrencyType = createPaymentRequest.CurrencyType;
            PaymentStatus = PaymentStatus.Processing;
        }
    }
}
