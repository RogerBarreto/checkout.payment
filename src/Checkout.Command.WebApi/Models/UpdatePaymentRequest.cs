namespace Checkout.Command.WebApi.Models
{
    public class UpdatePaymentRequest
    {
        public string PaymentStatus { get; set; }
        public string PaymentStatusDescription { get; set; }
        public string BankPaymentId { get; set; }
    }
}