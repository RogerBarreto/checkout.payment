
namespace Checkout.Payment.Gateway.Application
{
    public class UserTokenResponseModel
    {
        public string Token { get; }

        public UserTokenResponseModel(string token)
        {
            Token = token;
        }
    }
}
