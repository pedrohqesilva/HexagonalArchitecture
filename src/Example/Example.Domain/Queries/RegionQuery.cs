using ConsoleAppRE;
using Core.Domain.Specifications;
using Example.Domain.Interfaces.Queries;
using Example.Domain.Interfaces.Repositories;
using Example.Domain.Queries.Specification;
using System.Threading;
using System.Threading.Tasks;

namespace Example.Domain.Queries
{
    public class RegionQuery : IRegionQuery
    {
        private readonly IRegionRepository _regionRepository;

        public RegionQuery(IRegionRepository regionRepository)
        {
            _regionRepository = regionRepository;
        }

        public Task<bool> Exists(decimal id, CancellationToken cancellationToken)
        {
            var spec = SpecificationBuilder<Regions>.Create()
                .WithId(id);

            return _regionRepository.AnyAsync(spec, cancellationToken);
        }

        public Task<Regions> Get(decimal id, CancellationToken cancellationToken)
        {
            return _regionRepository.FindAsync(cancellationToken, id);
        }
    }
}