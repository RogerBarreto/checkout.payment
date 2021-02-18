using Checkout.Payment.Command.Domain.Models;
using Checkout.Payment.Command.Seedwork.Extensions;
using System;
using System.Threading.Tasks;

namespace Checkout.Payment.Command.Domain.Interfaces
{
    public interface IPaymentRepository
    {
        Task<ITryResult<PaymentRequest>> TryCreatePayment(CreatePaymentCommand command);
		Task<ITryResult> TryRemovePayment(Guid paymentId);
	}
}
