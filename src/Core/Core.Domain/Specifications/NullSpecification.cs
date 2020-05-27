using System;
using System.Linq.Expressions;

namespace Core.Domain.Specifications
{
    public class NullSpecification<T> : SpecificationBuilder<T>
    {
        public override Expression<Func<T, bool>> Predicate { get; }

        public NullSpecification()
        {
        }
    }
}