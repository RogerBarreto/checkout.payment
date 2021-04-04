namespace Checkout.Application.Common.Models.Shared
{
    public struct NotificationSuccess
    {
        public string MessageId { get; }
        public NotificationSuccess(string messageId)
        {
            MessageId = messageId;
        }
    }
}