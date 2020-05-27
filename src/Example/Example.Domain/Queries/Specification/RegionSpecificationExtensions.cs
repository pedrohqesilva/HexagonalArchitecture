using ConsoleAppRE;
using Core.Domain.Interfaces;

namespace Example.Domain.Queries.Specification
{
    public static class RegionSpecificationExtensions
    {
        public static ISpecification<Regions> WithId(this ISpecification<Regions> spec, decimal id)
        {
            return spec.And(x => x.RegionId == id);
        }
    }
}