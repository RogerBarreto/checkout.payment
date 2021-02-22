using System.Threading.Tasks;

namespace Checkout.Payment.Gateway.Application.Interfaces
{
    public interface IAuthenticationService
    {
        Task<string> UserGetTokenAsync(UserTokenRequestModel requestModel);
		Task<string> ApiGetTokenAsync(ApiTokenRequestModel requestModel);
	}
}
