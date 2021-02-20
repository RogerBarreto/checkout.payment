using Checkout.Payment.Command.Application.Models;
using Checkout.Payment.Command.Seedwork.Extensions;
using System;
using System.Threading.Tasks;

namespace Checkout.Payment.Command.Application.Interfaces
{
    public interface IPaymentService
    {
        Task<ITryResult<CreatePaymentResponseModel>> TryCreatePaymentAsync(int merchantId, CreatePaymentRequestModel requestModel);
        Task<ITryResult<bool>> TryUpdatePaymentAsync(Guid paymentId, UpdatePaymentRequestModel requestModel);
    }
}
