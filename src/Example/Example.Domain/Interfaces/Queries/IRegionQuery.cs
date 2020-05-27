using ConsoleAppRE;
using System.Threading;
using System.Threading.Tasks;

namespace Example.Domain.Interfaces.Queries
{
    public interface IRegionQuery
    {
        Task<bool> Exists(decimal id, CancellationToken cancellationToken);

        Task<Regions> Get(decimal id, CancellationToken cancellationToken);
    }
}