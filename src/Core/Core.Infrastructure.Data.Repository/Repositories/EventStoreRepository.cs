using Core.Domain.Events;
using Core.Domain.Interfaces;
using Core.Infrastructure.Data.Context;

namespace Core.Infrastructure.Data.Repository.Repositories
{
    public class EventStoreRepository : ReadWriteRepository<EventLog>, IEventStoreRepository
    {
        public EventStoreRepository(EventStoreContext context)
            : base(context)
        {
        }
    }
}