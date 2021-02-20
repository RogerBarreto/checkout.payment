using System;

namespace Checkout.Payment.Command.Domain
{
    public class UpdatePaymentCommandResponse
    {
        public Guid PaymentId { get; }
        public bool UpdateSucessful { get; }
        public UpdatePaymentCommandResponse(Guid paymentId, bool updateSucessful)
        {
            PaymentId = paymentId;
            UpdateSucessful = updateSucessful;
        }
    }
}
