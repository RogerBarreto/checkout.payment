using System;
using System.Threading.Tasks;
using Checkout.Application.Common.Models.Payments;
using Checkout.Application.Common.Models.Payments.Commands;
using Checkout.Command.Application.Payments.Commands;
using OneOf;

namespace Checkout.Command.Application.Common.Interfaces
{
	public interface IPaymentRepository
	{
		Task<OneOf<Guid, PaymentError>> CreatePaymentAsync(CreatePaymentCommand create);
		Task<OneOf<PaymentUpdated, PaymentNotFound, PaymentError>> UpdatePaymentAsync(UpdatePaymentCommand update);
		Task<OneOf<PaymentDeleted, PaymentNotFound, PaymentError>> DeletePaymentAsync(Guid paymentId);
	}
}
