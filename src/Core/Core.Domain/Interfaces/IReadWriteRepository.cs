namespace Core.Domain.Interfaces
{
    public interface IReadWriteRepository<T> : IReadRepository<T>, IWriteRepository<T>
    {
    }
}