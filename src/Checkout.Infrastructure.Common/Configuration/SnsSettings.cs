namespace Checkout.Infrastructure.Common.Configuration
{
    public interface ISnsSettings
    {
        string ServiceURL { get; }
        bool UseHttp { get; }
        string ProxyHost { get; }
        int ProxyPort { get; }
        string AccessKey { get; }
        string SecretKey { get; }
        string NotifyPaymentTopicArn { get; }
    }
}