using Checkout.Gateway.Application.Authentication.Errors;
using OneOf;
using System.Threading.Tasks;

namespace Checkout.Gateway.Application.Common.Interfaces
{
	public interface IAuthenticationClient
	{
		Task<OneOf<string, AuthenticationError>> GetUserTokenAsync(string userName, string password);
		Task<OneOf<string, AuthenticationError>> GetApiTokenAsync(string apiKey, string apiSecret);
	}
}
