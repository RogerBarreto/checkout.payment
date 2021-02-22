using Checkout.Payment.Query.Seedwork.Models;
using System.Collections.Generic;

namespace Checkout.Payment.Query.Seedwork.Interfaces
{
    public interface IDomainNotification
    {
        void Publish(IDomainNotificationEvent @event);
        void PublishBusinessViolation(string message);
        void PublishError(string message);

        public bool HasNotificationType(DomainNotificationType type);
        public IEnumerable<IDomainNotificationEvent> GetNotifications(DomainNotificationType type);
		void PublishNotFound(string message);
	}
}