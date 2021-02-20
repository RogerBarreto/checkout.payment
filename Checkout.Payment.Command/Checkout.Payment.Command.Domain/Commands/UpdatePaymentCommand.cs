using Checkout.Payment.Command.Domain.Models.Enums;
using Checkout.Payment.Command.Seedwork.Extensions;
using MediatR;
using System;

namespace Checkout.Payment.Command.Domain
{
    public class UpdatePaymentCommand : IRequest<ITryResult<UpdatePaymentCommandResponse>>
    {
        public Guid PaymentId { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public string PaymentStatusDetails { get; set; }
        public string BankPaymentId { get; set; }
    }
}
