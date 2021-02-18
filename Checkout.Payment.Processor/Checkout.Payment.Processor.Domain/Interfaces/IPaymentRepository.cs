using Checkout.Payment.Processor.Domain.Models;
using Checkout.Payment.Processor.Seedwork.Extensions;
using System;
using System.Threading.Tasks;

namespace Checkout.Payment.Processor.Domain.Interfaces
{
    public interface IPaymentRepository
    {
        Task<ITryResult<PaymentMessage>> TryCreatePayment(CreatePaymentCommand command);
		Task<ITryResult> TryRemovePayment(Guid paymentId);
	}
}
