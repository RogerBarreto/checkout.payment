﻿using System.Runtime.Serialization;

namespace Checkout.Payment.Gateway.MicroServices.Configurations
{
    public class MicroServiceSettings
    {
        public string IdentityBaseAddress { get; set; }
        public string PaymentCommandBaseAddress { get; set; }
		public string PaymentQueryBaseAddress { get; set; }
	}
}
