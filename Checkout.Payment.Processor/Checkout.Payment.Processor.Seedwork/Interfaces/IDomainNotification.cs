﻿using Checkout.Payment.Processor.Seedwork.Models;
using System.Collections.Generic;

namespace Checkout.Payment.Processor.Seedwork.Interfaces
{
    public interface IDomainNotification
    {
        void Publish(IDomainNotificationEvent @event);
        void PublishBusinessViolation(string error);
        void PublishError(string error);

        public bool HasNotificationType(DomainNotificationType type);
        public IEnumerable<IDomainNotificationEvent> GetNotifications(DomainNotificationType type);

    }
}