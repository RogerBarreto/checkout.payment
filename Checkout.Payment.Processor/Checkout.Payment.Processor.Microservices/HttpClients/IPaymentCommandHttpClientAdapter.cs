using Checkout.Payment.Processor.Domain.Models;
using Checkout.Payment.Processor.Domain.Models.PaymentCommand;
using Checkout.Payment.Processor.Seedwork.Extensions;
using System;
using System.Threading.Tasks;

namespace Checkout.Payment.Processor.MicroServices.HttpClients
{
    public interface IPaymentCommandHttpClientAdapter
    {
        Task<ITryResult<UpdatePaymentResponse>> TryUpdatePayment(UpdatePaymentRequest request);
    }
}
