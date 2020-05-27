using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;

namespace Core.Test.Reflection
{
    public static class InjectedInterfaces
    {
        public static IEnumerable<Type> Get(string assembly, string package)
        {
            var interfaces = Assembly.Load(assembly).GetTypes()
                                     .Where(x => x.Namespace == package
                                              && x.IsInterface);

            return interfaces;
        }

        public static void Verify(IServiceProvider serviceProvider, string assembly, string package)
        {
            var interfaces = Get(assembly, package);

            foreach (Type type in interfaces)
            {
                var service = serviceProvider.GetService(type);
                Assert.NotNull(service);
            }
        }
    }
}