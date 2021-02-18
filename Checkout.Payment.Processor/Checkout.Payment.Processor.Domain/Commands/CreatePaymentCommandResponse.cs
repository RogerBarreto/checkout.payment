using Checkout.Payment.Processor.Domain.Models.Enums;
using MediatR;
using System;

namespace Checkout.Payment.Processor.Domain
{
    public class CreatePaymentCommandResponse
    {
        public Guid PaymentId { get; }
        public CreatePaymentCommandResponse(Guid paymentId)
        {
            PaymentId = paymentId;
        }
    }
}
