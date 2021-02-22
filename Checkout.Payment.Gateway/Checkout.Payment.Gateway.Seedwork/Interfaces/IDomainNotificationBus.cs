using Checkout.Payment.Gateway.Seedwork.Models;
using System.Collections.Generic;

namespace Checkout.Payment.Gateway.Seedwork.Interfaces
{
    public interface IDomainNotificationBus
    {
        void Publish(IDomainNotificationEvent @event);
        void PublishBusinessViolation(string message);
        void PublishError(string message);

        public bool HasNotificationType(DomainNotificationType type);
        public IEnumerable<IDomainNotificationEvent> GetNotifications(DomainNotificationType type);
		void PublishNotFound(string message);
	}
}