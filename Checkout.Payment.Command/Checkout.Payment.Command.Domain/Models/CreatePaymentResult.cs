using System;

namespace Checkout.Payment.Command.Domain.Models
{
    public class CreatePaymentResult
    {
        public Guid PaymentId { get; }
        public CreatePaymentResult(Guid paymentId)
        {
            PaymentId = paymentId;
        }
    }
}
