using System;
using Checkout.Domain.Enums;
using FluentValidation;

namespace Checkout.Command.Application.Payments.Commands
{
	public class UpdatePaymentCommandValidator : AbstractValidator<UpdatePaymentCommand>
	{
		public UpdatePaymentCommandValidator()
		{
			RuleFor(o => o.PaymentStatus)
				.Must(ValidatePaymentStatus);

			RuleFor(o => o.BankPaymentId)
				.NotNull()
				.NotEmpty();

			RuleFor(o => o.PaymentId)
				.NotEmpty();
		}

		private bool ValidatePaymentStatus(PaymentStatus status)
		{

			return Enum.IsDefined(typeof(PaymentStatus), status);
		}
	}
}
