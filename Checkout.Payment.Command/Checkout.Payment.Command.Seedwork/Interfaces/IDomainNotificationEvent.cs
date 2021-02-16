using Checkout.Payment.Gateway.Seedwork.Models;

namespace Checkout.Payment.Gateway.Seedwork.Interfaces
{
    public interface IDomainNotificationEvent
    {
        string Message { get; set; }
        DomainNotificationType Type { get; set; }
    }
}