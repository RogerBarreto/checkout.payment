using Checkout.Payment.Query.Application.Models;
using Checkout.Payment.Query.Seedwork.Extensions;
using System;
using System.Threading.Tasks;

namespace Checkout.Payment.Query.Application.Interfaces
{
    public interface IPaymentService
    {
        Task<ITryResult<GetPaymentResponseModel>> TryGetPaymentAsync(int merchantId, Guid paymentId);
    }
}
