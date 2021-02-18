using Checkout.Payment.Processor.Seedwork.Interfaces;

namespace Checkout.Payment.Processor.Seedwork.Models
{
    public class DomainNotificationEvent : IDomainNotificationEvent
    {
        public DomainNotificationType Type { get; set; }
        public string Message { get; set; }
    }
}
