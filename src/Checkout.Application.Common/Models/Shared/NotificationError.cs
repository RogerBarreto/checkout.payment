namespace Checkout.Application.Common.Models.Shared
{
    public struct NotificationError
    {
        public string Message { get; }
        public NotificationError(string message)
        {
            Message = message;
        }
    }
}