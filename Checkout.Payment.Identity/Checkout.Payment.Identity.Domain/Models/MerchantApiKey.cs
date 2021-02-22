using System;

namespace Checkout.Payment.Identity.Domain.Models
{
    public class MerchantApiKey
    {
        public string SubjectId { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
		public string ApiKey { get; set; }
	}
}
