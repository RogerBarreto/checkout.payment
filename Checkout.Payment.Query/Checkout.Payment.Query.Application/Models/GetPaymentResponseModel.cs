using Checkout.Payment.Query.Application.Models.Enums;
using Checkout.Payment.Query.Domain;
using System;

namespace Checkout.Payment.Query.Application.Models
{
    public class GetPaymentResponseModel
    {
        public Guid PaymentId { get; set; }
        public string CardNumber { get; set; }
        public int CardExpiryMonth { get; set; }
        public int CardExpiryYear { get; set; }
        public decimal Amount { get; set; }
        public CurrencyTypeModel CurrencyType { get; set; }
        public PaymentStatusModel PaymentStatus { get; set; }
        public string PaymentStatusDetails { get; set; }

        public GetPaymentResponseModel(GetPaymentQueryResponse command)
        {
            PaymentId = command.PaymentId;
            PaymentStatus = Enum.Parse<PaymentStatusModel>(command.PaymentStatus.ToString());
            PaymentStatusDetails = command.PaymentStatusDetails;
            CardNumber = command.CardNumber;
            CardExpiryMonth = command.ExpiryDate.Month;
            CardExpiryYear = command.ExpiryDate.Year;
            Amount = command.Amount;
            CurrencyType = Enum.Parse<CurrencyTypeModel>(command.CurrencyType.ToString());
        }
    }
}
