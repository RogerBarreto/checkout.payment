using Checkout.Payment.Query.Seedwork.Interfaces;

namespace Checkout.Payment.Query.Seedwork.Models
{
    public class DomainNotificationEvent : IDomainNotificationEvent
    {
        public DomainNotificationType Type { get; set; }
        public string Message { get; set; }
    }
}
