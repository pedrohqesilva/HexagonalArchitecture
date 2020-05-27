using Core.Domain.Commands;
using Core.Domain.Events;
using Core.Domain.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Infrastructure.CrossCutting.Bus
{
    public sealed class InMemoryBus : IMediatorHandler
    {
        private readonly IMediator _mediator;
        private readonly IEventStore _eventStore;

        public InMemoryBus(IMediator mediator, IEventStore eventStore)
        {
            _eventStore = eventStore;
            _mediator = mediator;
        }

        public async Task<TResponse> SendCommandAsync<TCommand, TResponse>(TCommand command, CancellationToken cancellationToken) where TCommand : Command<TResponse>
        {
            var result = await _mediator.Send(command, cancellationToken);
            return result;
        }

        public async Task RaiseEventAsync<T>(T @event, CancellationToken cancellationToken) where T : Event
        {
            try
            {
                await _eventStore.AddAsync(@event, cancellationToken);
                await _mediator.Publish(@event);

                await _eventStore.MarkEventAsPublishedAsync(@event.Id, cancellationToken);
            }
            catch (Exception ex)
            {
                await _eventStore.MarkEventAsFailedAsync(@event.Id, cancellationToken);
                throw ex;
            }
        }

        public Task RaiseNotificationAsync(DomainNotification notification, CancellationToken cancellationToken)
        {
            return _mediator.Publish(notification, cancellationToken);
        }
    }
}