using Core.Domain.Events;
using Core.Domain.Interfaces;
using Moq;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Test.Mock
{
    public static class MockMediatorHandler
    {
        public static IMediatorHandler Get()
        {
            var mediatorHandler = new Mock<IMediatorHandler>();
            mediatorHandler.Setup(x => x.RaiseEventAsync(It.IsAny<Event>(), It.IsAny<CancellationToken>()))
                           .Returns(Task.FromResult<Event>(null));

            return mediatorHandler.Object;
        }
    }
}