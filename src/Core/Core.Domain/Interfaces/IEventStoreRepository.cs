using Core.Domain.Events;

namespace Core.Domain.Interfaces
{
    public interface IEventStoreRepository : IReadWriteRepository<EventLog>
    {
    }
}