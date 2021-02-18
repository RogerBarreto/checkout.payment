using System;

namespace Checkout.Payment.Command.Application.Models
{
    public class CreatePaymentResponseModel
    {
        public CreatePaymentResponseModel(Guid paymentId)
        {
            PaymentId = paymentId;
        }

        public Guid PaymentId { get; set; }
    }
}
