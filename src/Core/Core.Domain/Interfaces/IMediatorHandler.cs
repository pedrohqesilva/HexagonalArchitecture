using Core.Domain.Commands;
using Core.Domain.Events;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Domain.Interfaces
{
    public interface IMediatorHandler
    {
        Task<TResponse> SendCommandAsync<TCommand, TResponse>(TCommand command, CancellationToken cancellationToken) where TCommand : Command<TResponse>;

        Task RaiseEventAsync<T>(T @event, CancellationToken cancellationToken) where T : Event;

        Task RaiseNotificationAsync(DomainNotification notification, CancellationToken cancellationToken);
    }
}