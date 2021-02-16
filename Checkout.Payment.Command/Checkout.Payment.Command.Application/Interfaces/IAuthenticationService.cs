using System.Threading.Tasks;

namespace Checkout.Payment.Gateway.Application.Interfaces
{
    public interface IAuthenticationService
    {
        Task<string> LoginGetTokenAsync(UserTokenRequestModel loginRequestModel);
    }
}
