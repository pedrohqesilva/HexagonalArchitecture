using System.Threading;
using System.Threading.Tasks;

namespace Core.Domain.Interfaces
{
    public interface ISequenceRepository
    {
        Task<long> ObterProxima(string sequenceName, CancellationToken cancellationToken);
    }
}