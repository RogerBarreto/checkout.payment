
namespace Checkout.Payment.Gateway.Application
{
    public class ApiTokenResponseModel
    {
        public string Token { get; }

        public ApiTokenResponseModel(string token)
        {
            Token = token;
        }
    }
}
