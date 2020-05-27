using Core.Domain.Events;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Domain.Interfaces
{
    public interface IEventStore
    {
        Task AddAsync<T>(T theEvent, CancellationToken cancellationToken) where T : Event;

        Task MarkEventAsPublishedAsync(Guid eventId, CancellationToken cancellationToken);

        Task MarkEventAsProcessedAsync(Guid eventId, CancellationToken cancellationToken);

        Task MarkEventAsFailedAsync(Guid eventId, CancellationToken cancellationToken);
    }
}