namespace Checkout.Command.WebApi.Models
{
    public class CreatePaymentRequest
    {
        public string CardNumber { get; set; }
        public int CardCVV { get; set; }
        public int CardExpiryYear { get; set; }
        public int CardExpiryMonth { get; set; }
        public decimal Amount { get; set; }
        public string CurrencyType { get; set; }
    }
}