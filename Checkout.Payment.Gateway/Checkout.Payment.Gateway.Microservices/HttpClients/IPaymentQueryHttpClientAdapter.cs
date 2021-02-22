using Checkout.Payment.Gateway.Domain.Models;
using Checkout.Payment.Gateway.Seedwork.Extensions;
using System.Threading.Tasks;

namespace Checkout.Payment.Gateway.MicroServices.HttpClients
{
    public interface IPaymentQueryHttpClientAdapter
    {
        Task<ITryResult<GetPaymentResult>> TryGetPayment(GetPayment request);
    }
}
