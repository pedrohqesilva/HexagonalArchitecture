using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;
using Xunit.Abstractions;
using Xunit.DependencyInjection;

[assembly: TestFramework("Example.Domain.UnitTest.IoC.StartupTest", "Example.Domain.UnitTest")]

namespace Example.Domain.UnitTest.IoC
{
    public class StartupTest : DependencyInjectionTestFramework
    {
        public IServiceProvider ServiceProvider { get; private set; }

        public StartupTest(IMessageSink messageSink) : base(messageSink)
        {
        }

        protected override void ConfigureServices(IServiceCollection services)
        {
            Core.Infrastructure.CrossCutting.IoC.InjectorContainer.Register(services, null, null);
            Infrastructure.CrossCutting.IoC.InjectorContainer.Register(services, null);
            ServiceProvider = services.BuildServiceProvider();
        }
    }
}