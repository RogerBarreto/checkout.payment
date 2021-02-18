using Checkout.Payment.Command.Seedwork.Interfaces;

namespace Checkout.Payment.Command.Seedwork.Models
{
    public class DomainNotificationEvent : IDomainNotificationEvent
    {
        public DomainNotificationType Type { get; set; }
        public string Message { get; set; }
    }
}
