using ConsoleAppRE;
using Core.Domain.Specifications;
using Example.Domain.Interfaces.Queries;
using Example.Domain.Interfaces.Repositories;
using Example.Domain.Queries.Specification;
using System.Threading;
using System.Threading.Tasks;

namespace Example.Domain.Queries
{
    public class CountryQuery : ICountryQuery
    {
        private readonly ICountryRepository _countryRepository;

        public CountryQuery(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public Task<bool> Exists(string id, CancellationToken cancellationToken)
        {
            var spec = SpecificationBuilder<Countries>.Create()
                .WithId(id);

            return _countryRepository.AnyAsync(spec, cancellationToken);
        }

        public Task<Countries> Get(string id, CancellationToken cancellationToken)
        {
            var spec = SpecificationBuilder<Countries>.Create()
                .WithId(id);

            return _countryRepository.FirstOrDefaultAsync(spec, cancellationToken);
        }
    }
}