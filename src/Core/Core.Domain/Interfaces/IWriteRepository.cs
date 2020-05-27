using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Domain.Interfaces
{
    public interface IWriteRepository<T>
    {
        T Add(T entity);

        Task AddRange(IList<T> entity, CancellationToken cancellationToken);

        void Remove(T entity);

        void Update(T entity);

        void Attach(T entity);

        Task<int> SaveChanges(CancellationToken cancellationToken);
    }
}