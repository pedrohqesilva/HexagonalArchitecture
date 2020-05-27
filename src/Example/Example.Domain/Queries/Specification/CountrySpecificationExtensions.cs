using ConsoleAppRE;
using Core.Domain.Interfaces;

namespace Example.Domain.Queries.Specification
{
    public static class CountrySpecificationExtensions
    {
        public static ISpecification<Countries> WithId(this ISpecification<Countries> spec, string id)
        {
            return spec.And(x => x.CountryId == id);
        }
    }
}