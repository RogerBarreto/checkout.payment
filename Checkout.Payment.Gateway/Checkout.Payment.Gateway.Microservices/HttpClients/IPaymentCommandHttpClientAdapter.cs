using Checkout.Payment.Gateway.Domain.Models;
using Checkout.Payment.Gateway.Seedwork.Extensions;
using System;
using System.Threading.Tasks;

namespace Checkout.Payment.Gateway.MicroServices.HttpClients
{
    public interface IPaymentCommandHttpClientAdapter
    {
        Task<ITryResult<CreatePaymentResult>> TryCreatePayment(CreatePayment request);
    }
}
