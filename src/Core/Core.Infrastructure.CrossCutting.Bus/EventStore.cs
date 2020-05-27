using Core.Domain.Events;
using Core.Domain.Interfaces;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Infrastructure.CrossCutting.Bus
{
    public class EventStore : IEventStore
    {
        private readonly IEventStoreRepository _eventStoreRepository;

        public EventStore(IEventStoreRepository eventStoreRepository)
        {
            _eventStoreRepository = eventStoreRepository;
        }

        public async Task MarkEventAsFailedAsync(Guid eventId, CancellationToken cancellationToken)
        {
            var @event = await _eventStoreRepository.FindAsync(cancellationToken, eventId);
            @event.MarkEventAsFailed();

            _eventStoreRepository.Update(@event);
            await _eventStoreRepository.SaveChanges(cancellationToken);
        }

        public async Task MarkEventAsProcessedAsync(Guid eventId, CancellationToken cancellationToken)
        {
            var @event = await _eventStoreRepository.FindAsync(cancellationToken, eventId);
            @event.MarkEventAsProcessed();

            _eventStoreRepository.Update(@event);
            await _eventStoreRepository.SaveChanges(cancellationToken);
        }

        public async Task MarkEventAsPublishedAsync(Guid eventId, CancellationToken cancellationToken)
        {
            var @event = await _eventStoreRepository.FindAsync(cancellationToken, eventId);
            @event.MarkEventAsPublished();

            _eventStoreRepository.Update(@event);
            await _eventStoreRepository.SaveChanges(cancellationToken);
        }

        public async Task AddAsync<T>(T @event, CancellationToken cancellationToken) where T : Event
        {
            var serializedData = JsonConvert.SerializeObject(@event);

            var storedEvent = new EventLog(@event.Id, @event.Timestamp, @event.GetType().FullName, serializedData);

            _eventStoreRepository.Add(storedEvent);
            await _eventStoreRepository.SaveChanges(cancellationToken);
        }
    }
}