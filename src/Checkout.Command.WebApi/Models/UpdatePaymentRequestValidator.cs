using System;
using Checkout.Domain.Enums;
using FluentValidation;

namespace Checkout.Command.WebApi.Models
{
    public class UpdatePaymentRequestValidator : AbstractValidator<UpdatePaymentRequest>
    {
        public UpdatePaymentRequestValidator()
        {
            RuleFor(o => o.PaymentStatus)
                .Must(status => Enum.IsDefined(typeof(PaymentStatus), status));

            RuleFor(o => o.BankPaymentId)
                .NotNull()
                .NotEmpty();

            RuleFor(o => o.PaymentStatusDescription)
                .NotNull()
                .NotEmpty();
        }
    }
}