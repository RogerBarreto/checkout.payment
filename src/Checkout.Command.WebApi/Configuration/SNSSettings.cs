using Checkout.Infrastructure.Common.Configuration;

namespace Checkout.Command.WebApi.Configuration
{
    public class SnsSettings : ISnsSettings
    {
        public string ServiceURL { get; set; }
        public bool UseHttp { get; set; }
        public string ProxyHost { get; set; }
        public int ProxyPort { get; set; }
        public string AccessKey { get; set;  }
        public string SecretKey { get; set;  }
        public string NotifyPaymentTopicArn { get; set;  }
    }
}