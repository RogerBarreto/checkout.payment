using Checkout.Payment.Processor.Domain.Models.AcquiringBank;
using Checkout.Payment.Processor.Seedwork.Extensions;
using System.Threading.Tasks;

namespace Checkout.Payment.Processor.Domain.Interfaces
{
    public interface IAcquiringBankHttpClientAdapter
    {
        Task<ITryResult<IBankPaymentResponse>> TrySendPayment(IBankPaymentRequest request);
    }
}
