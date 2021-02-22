using Checkout.Payment.Gateway.Seedwork.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Checkout.Payment.Gateway.Seedwork.Models
{
    public class DomainNotificationBus : IDomainNotificationBus
    {
        private List<IDomainNotificationEvent> _notifications = new List<IDomainNotificationEvent>();

        public IEnumerable<IDomainNotificationEvent> GetNotifications(DomainNotificationType type)
        {
            return _notifications.Where(n => n.Type == type);
        }

        public bool HasNotificationType(DomainNotificationType type)
        {
            return _notifications.Any(n => n.Type == type);
        }

        public void Publish(IDomainNotificationEvent @event)
        {
            _notifications.Add(@event);
        }

        public void PublishBusinessViolation(string message)
        {
            _notifications.Add(new DomainNotificationEvent
            {
                Type = DomainNotificationType.BusinessViolation,
                Message = message
            });
        }

        public void PublishError(string message)
        {
            _notifications.Add(new DomainNotificationEvent
            {
                Type = DomainNotificationType.Error,
                Message = message
            });
        }

		public void PublishNotFound(string message)
		{
            _notifications.Add(new DomainNotificationEvent
            {
                Type = DomainNotificationType.NotFound,
                Message = message
            });
        }
	}
}
