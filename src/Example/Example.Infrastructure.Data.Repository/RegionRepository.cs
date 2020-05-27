using ConsoleAppRE;
using Core.Infrastructure.Data.Repository.Repositories;
using Example.Domain.Interfaces.Repositories;
using Example.Infrastructure.Data.Context;

namespace Example.Infrastructure.Data.Repository
{
    public class RegionRepository : ReadWriteRepository<Regions>, IRegionRepository
    {
        public RegionRepository(ExampleContext context) : base(context)
        {
        }
    }
}