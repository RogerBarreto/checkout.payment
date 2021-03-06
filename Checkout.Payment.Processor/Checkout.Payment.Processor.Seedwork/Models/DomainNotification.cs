﻿using Checkout.Payment.Processor.Seedwork.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Checkout.Payment.Processor.Seedwork.Models
{
    public class DomainNotification : IDomainNotification
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

        public void PublishBusinessViolation(string error)
        {
            _notifications.Add(new DomainNotificationEvent
            {
                Type = DomainNotificationType.BusinessViolation,
                Message = error
            });
        }

        public void PublishError(string error)
        {
            _notifications.Add(new DomainNotificationEvent
            {
                Type = DomainNotificationType.Error,
                Message = error
            });
        }
    }
}
