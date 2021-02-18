namespace Checkout.Payment.Processor.Data.Configurations
{
	public class SNSSettings
	{
		public LocalCredentials Credentials { get; set; }
		public string Region { get; set; }
		public bool IsEnabled { get; set; }
		public string NotifyPaymentTopicArn { get; set; }
		public string ServiceURL { get; set; }
		public bool UseHttp { get; set; }
		public string ProxyHost { get; set; }
		public int ProxyPort { get; set; }

		public class LocalCredentials
		{
			public string AccessKey { get; set; }
			public string SecretKey { get; set; }
		}
	}
}
