using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Checkout.Payment.Processor.Domain.Configurations
{
    public class MicroServiceSettings
    {
        public string PaymentCommandBaseAddress { get; set; }
		public string AcquiringBankBaseAddress { get; internal set; }
		public string AcquiringBankAuthorization { get; internal set; }
	}
}
