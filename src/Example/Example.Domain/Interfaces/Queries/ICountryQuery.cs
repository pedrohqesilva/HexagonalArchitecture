using ConsoleAppRE;
using System.Threading;
using System.Threading.Tasks;

namespace Example.Domain.Interfaces.Queries
{
    public interface ICountryQuery
    {
        Task<bool> Exists(string id, CancellationToken cancellationToken);

        Task<Countries> Get(string id, CancellationToken cancellationToken);
    }
}