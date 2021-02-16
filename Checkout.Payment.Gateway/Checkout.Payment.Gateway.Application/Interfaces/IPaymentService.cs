using Checkout.Payment.Gateway.Application.Models;
using System.Threading.Tasks;

namespace Checkout.Payment.Gateway.Application.Interfaces
{
    public interface IPaymentService
    {
        Task<CreatePaymentResponseModel> CreatePaymentAsync(int merchantId, CreatePaymentRequestModel requestModel);
    }
}
