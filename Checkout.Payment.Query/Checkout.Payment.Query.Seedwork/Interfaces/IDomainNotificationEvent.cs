using Checkout.Payment.Query.Seedwork.Models;

namespace Checkout.Payment.Query.Seedwork.Interfaces
{
    public interface IDomainNotificationEvent
    {
        string Message { get; set; }
        DomainNotificationType Type { get; set; }
    }
}