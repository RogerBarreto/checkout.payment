using Checkout.Payment.Command.Application.Models.Enums;

namespace Checkout.Payment.Command.Application.Models
{
    public class UpdatePaymentRequestModel
    {

        public PaymentStatusModel PaymentStatus { get; set; }
        public string PaymentStatusDetails { get; set; }
        public string BankPaymentId { get; set; }
    }
}
