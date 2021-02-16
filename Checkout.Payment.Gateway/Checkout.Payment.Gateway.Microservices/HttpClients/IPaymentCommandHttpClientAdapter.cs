using Checkout.Payment.Gateway.Domain;
using System;
using System.Threading.Tasks;

namespace Checkout.Payment.Gateway.MicroServices.HttpClients
{
    public interface IPaymentCommandHttpClientAdapter
    {
        Task<Guid> CreatePayment(CreatePayment request);
    }
}
