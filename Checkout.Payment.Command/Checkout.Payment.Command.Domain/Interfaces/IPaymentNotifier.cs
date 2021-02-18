using Checkout.Payment.Command.Domain.Models;
using Checkout.Payment.Command.Seedwork.Extensions;
using System.Threading.Tasks;

namespace Checkout.Payment.Command.Domain.Interfaces
{
    public interface IPaymentNotifier
    {
        Task<ITryResult<string>> TryNotifyPaymentAsync(PaymentRequest request);
    }
}
