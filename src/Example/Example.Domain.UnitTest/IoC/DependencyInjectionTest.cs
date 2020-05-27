using Core.Test.Reflection;
using System;
using Xunit;

namespace Example.Domain.UnitTest.IoC
{
    public class DependencyInjectionTest
    {
        private readonly IServiceProvider _serviceProvider;

        public DependencyInjectionTest(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        [Fact]
        public void Example_repositories_interfaces_must_be_injected()
        {
            InjectedInterfaces.Verify(_serviceProvider, "Example.Domain", "Example.Domain.Interfaces.Repositories");
        }

        [Fact]
        public void Example_queries_interfaces_must_be_injected()
        {
            InjectedInterfaces.Verify(_serviceProvider, "Example.Domain", "Example.Domain.Interfaces.Queries");
        }
    }
}