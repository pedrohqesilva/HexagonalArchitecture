using ConsoleAppRE;
using Core.Domain.Interfaces;

namespace Example.Domain.Interfaces.Repositories
{
    public interface ICountryRepository : IReadWriteRepository<Countries>
    {
    }
}