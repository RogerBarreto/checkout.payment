using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Checkout.Payment.Processor.MicroServices.Configurations
{
    public class MicroServiceSettings
    {
        public string PaymentCommandBaseAddress { get; set; }
		public string AcquiringBankBaseAddress { get; set; }
		public string AcquiringBankAuthorization { get; set; }
	}
}
