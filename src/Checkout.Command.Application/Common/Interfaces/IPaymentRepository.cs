using System;
using System.Threading.Tasks;
using Checkout.Application.Common.Payments.Commands;
using Checkout.Command.Application.Payments.Commands;
using Checkout.Domain.Errors;
using OneOf;

namespace Checkout.Command.Application.Common.Interfaces
{
	public interface IPaymentRepository
	{
		Task<OneOf<Guid, PaymentError>> CreatePaymentAsync(CreatePaymentCommand create);
		Task<OneOf<PaymentUpdated, PaymentNotFound, PaymentError>> UpdatePaymentAsync(UpdatePaymentCommand update);
	}
}
