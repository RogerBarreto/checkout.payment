using Checkout.Payment.Query.Domain.Models;
using Checkout.Payment.Query.Seedwork.Extensions;
using System;
using System.Threading.Tasks;

namespace Checkout.Payment.Query.Domain.Interfaces
{
    public interface IPaymentRepository
    {
        Task<ITryResult<PaymentRequest>> TryGetPayment(Guid paymentId);
	}
}
