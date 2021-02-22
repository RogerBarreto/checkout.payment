
using System.ComponentModel.DataAnnotations;

namespace Checkout.Payment.Gateway.Application
{
    public class ApiTokenRequestModel
    {
        [Required]
        public string ApiKey { get; set; }

        [MinLength(8)]
        public string ApiSecret { get; set; }
    }
}
