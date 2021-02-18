using Checkout.Payment.Processor.Domain.Models;
using Checkout.Payment.Processor.Seedwork.Extensions;
using System.Threading.Tasks;

namespace Checkout.Payment.Processor.Domain.Interfaces
{
    public interface IPaymentNotifier
    {
        Task<ITryResult<string>> TryNotifyPaymentAsync(PaymentMessage request);
    }
}
