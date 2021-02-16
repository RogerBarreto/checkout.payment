using System.Threading.Tasks;

namespace Checkout.Payment.Gateway.MicroServices.HttpClients
{
    public interface IPaymentIdentityHttpClientAdapter
    {
        Task<string> GetTokenAsync(string userName, string password);
    }
}
