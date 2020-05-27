using Core.Domain.Events;
using MediatR;
using System;
using System.Collections.Generic;

namespace Core.Domain.Interfaces
{
    public interface IDomainNotificationHandler : INotificationHandler<DomainNotification>, IDisposable
    {
        List<DomainNotification> GetNotifications();

        bool HasNotifications();
    }
}