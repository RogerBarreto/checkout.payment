
using System.ComponentModel.DataAnnotations;

namespace Checkout.Payment.Gateway.Application
{
    public class UserTokenRequestModel
    {
        [Required]
        public string UserName { get; set; }

        [MinLength(8)]
        public string Password { get; set; }
    }
}
