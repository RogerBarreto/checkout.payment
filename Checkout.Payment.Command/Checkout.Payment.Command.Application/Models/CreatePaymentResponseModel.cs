using System;

namespace Checkout.Payment.Gateway.Application.Models
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
