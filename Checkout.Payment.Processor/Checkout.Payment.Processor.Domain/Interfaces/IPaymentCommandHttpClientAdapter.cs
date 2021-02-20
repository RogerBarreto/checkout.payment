using Checkout.Payment.Processor.Domain.Models.PaymentCommand;
using Checkout.Payment.Processor.Seedwork.Extensions;
using System.Threading.Tasks;

namespace Checkout.Payment.Processor.Domain.Interfaces
{
    public interface IPaymentCommandHttpClientAdapter
    {
        Task<ITryResult<UpdatePaymentResponse>> TryUpdatePayment(UpdatePaymentRequest request);
    }
}
