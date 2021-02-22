using Checkout.Payment.Gateway.Application.Models.Enums;
using Checkout.Payment.Gateway.Domain.Models;
using System;

namespace Checkout.Payment.Gateway.Application.Models
{
    public class GetPaymentResponseModel
    {
        public Guid PaymentId { get; set; }
        public string MaskedCardNumber { get; set; }
        public int CardExpiryMonth { get; set; }
        public int CardExpiryYear { get; set; }
        public decimal Amount { get; set; }
        public CurrencyTypeModel CurrencyType { get; set; }
        public int PaymentStatusCode { get; set; }
        public string PaymentStatusDetails { get; set; }

        public GetPaymentResponseModel(GetPaymentResult result)
        {
            PaymentId = result.PaymentId;
            PaymentStatusCode = (int)Enum.Parse<PaymentStatusModel>(result.PaymentStatus.ToString());
            PaymentStatusDetails = result.PaymentStatusDetails;
            MaskedCardNumber = MaskCardNumber(result.CardNumber);
            CardExpiryMonth = result.CardExpiryMonth;
            CardExpiryYear = result.CardExpiryYear;
            Amount = result.Amount;
            CurrencyType = Enum.Parse<CurrencyTypeModel>(result.CurrencyType.ToString());
        }

        private string MaskCardNumber(string cardNumber)
        {
            return MaskInput(cardNumber, 4);
        }

        private string MaskInput(string input, int showLastNthDigits)
        {
            var numberOfDigits = input.Length;

            if (numberOfDigits > showLastNthDigits)
            {
                var maskedNumber = new string('*', numberOfDigits - 4);

                return $"{maskedNumber}{input.Substring(input.Length - 4, 4)}";
            }

            return input;
        }
    }
}
