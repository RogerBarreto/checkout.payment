using Checkout.Payment.Processor.Application.Models;
using Checkout.Payment.Processor.Seedwork.Extensions;
using System.Threading.Tasks;

namespace Checkout.Payment.Processor.Application.Interfaces
{
    public interface IPaymentService
    {
        Task<ITryResult<CreatePaymentResponseModel>> TryCreatePaymentAsync(int merchantId, CreatePaymentRequestModel requestModel);
    }
}
