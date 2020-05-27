using Core.Domain.Events;
using MediatR;
using Moq;

namespace Core.Test.Mock
{
    public static class MockDomainNotificationHandler
    {
        public static INotificationHandler<DomainNotification> Get()
        {
            return new Mock<INotificationHandler<DomainNotification>>().Object;
        }
    }
}