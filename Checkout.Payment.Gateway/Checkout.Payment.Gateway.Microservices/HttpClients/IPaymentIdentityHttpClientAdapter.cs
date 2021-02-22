using System.Threading.Tasks;

namespace Checkout.Payment.Gateway.MicroServices.HttpClients
{
    public interface IPaymentIdentityHttpClientAdapter
    {
        Task<string> GetUserTokenAsync(string userName, string password);
		Task<string> GetApiTokenAsync(string apiKey, string apiSecret);
	}
}
