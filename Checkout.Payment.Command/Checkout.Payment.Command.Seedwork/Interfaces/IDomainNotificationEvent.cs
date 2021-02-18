using Checkout.Payment.Command.Seedwork.Models;

namespace Checkout.Payment.Command.Seedwork.Interfaces
{
    public interface IDomainNotificationEvent
    {
        string Message { get; set; }
        DomainNotificationType Type { get; set; }
    }
}