using ConsoleAppRE;
using Core.Infrastructure.Data.Repository.Repositories;
using Example.Domain.Interfaces.Repositories;
using Example.Infrastructure.Data.Context;

namespace Example.Infrastructure.Data.Repository
{
    public class CountryRepository : ReadWriteRepository<Countries>, ICountryRepository
    {
        public CountryRepository(ExampleContext context) : base(context)
        {
        }
    }
}