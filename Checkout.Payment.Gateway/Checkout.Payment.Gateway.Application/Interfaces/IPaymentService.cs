using Checkout.Payment.Gateway.Application.Models;
using Checkout.Payment.Gateway.Seedwork.Extensions;
using System;
using System.Threading.Tasks;

namespace Checkout.Payment.Gateway.Application.Interfaces
{
    public interface IPaymentService
    {
        Task<ITryResult<CreatePaymentResponseModel>> TryCreatePaymentAsync(int merchantId, CreatePaymentRequestModel requestModel);
		Task<ITryResult<GetPaymentResponseModel>> TryGetPaymentAsync(int merchantId, Guid paymentId);
	}
}
