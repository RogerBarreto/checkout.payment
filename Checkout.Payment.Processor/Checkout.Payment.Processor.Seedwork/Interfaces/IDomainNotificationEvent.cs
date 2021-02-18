using Checkout.Payment.Processor.Seedwork.Models;

namespace Checkout.Payment.Processor.Seedwork.Interfaces
{
    public interface IDomainNotificationEvent
    {
        string Message { get; set; }
        DomainNotificationType Type { get; set; }
    }
}