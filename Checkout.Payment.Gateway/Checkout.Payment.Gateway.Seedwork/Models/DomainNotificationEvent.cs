using Checkout.Payment.Gateway.Seedwork.Interfaces;

namespace Checkout.Payment.Gateway.Seedwork.Models
{
    public class DomainNotificationEvent : IDomainNotificationEvent
    {
        public DomainNotificationType Type { get; set; }
        public string Message { get; set; }
    }
}
