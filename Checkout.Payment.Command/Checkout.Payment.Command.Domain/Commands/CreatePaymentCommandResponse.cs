using Checkout.Payment.Command.Domain.Models.Enums;
using MediatR;
using System;

namespace Checkout.Payment.Command.Domain
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
