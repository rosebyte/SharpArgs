using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using RoseByte.SharpArgs.Exceptions;
using RoseByte.SharpArgs.Internal.Helpers;
using RoseByte.SharpArgs.Internal.Parser;
using RoseByte.SharpArgs.Internal.Routers.BaseClasses;
using RoseByte.SharpArgs.Tests.TestObjects;
using SharpArgs.TestObjects;
using Xunit;

namespace RoseByte.SharpArgs.Tests.Extensions
{
    public class ServiceCollectionExtensionsTest
    {
        [Fact]
        public void ShouldThrowOnMultipleExecution()
        {
            var provider = new ServiceCollection();
            provider.UseSharpArgs<IRoute>();
            Assert.Throws<SharpArgsException>(() => provider.UseSharpArgs<IRoute>());
        }
        
        [Fact]
        public void ShouldUseEntryAssemblyAsDefault()
        {
            var provider = new ServiceCollection();
            provider.UseSharpArgs<IRoute>();
            Assert.Contains(provider, x => x.ServiceType == typeof(TestRoute));
        }
        
        [Fact]
        public void ShouldPopulateWithTypes()
        {
            var provider = new ServiceCollection();
            provider.UseSharpArgs<IRoute>(typeof(IRoute).Assembly);
            Assert.Contains(provider, x => x.ServiceType == typeof(TestRoute));
            Assert.DoesNotContain(provider, x => x.ServiceType == typeof(IgnoredRoute));
            Assert.DoesNotContain(provider, x => x.ServiceType == typeof(IRoute));
        }
        
        [Fact]
        public void ShouldPopulateTypeHelperWithTypes()
        {
            var provider = new ServiceCollection();
            provider.UseSharpArgs<IRoute>(typeof(IRoute).Assembly);

            var helper = (ITypeHelper<IRoute>)provider
                .BuildServiceProvider()
                .GetService(typeof(ITypeHelper<IRoute>));

            Assert.NotNull(helper);
            
            Assert.Contains("testroute", helper.Types);
            Assert.DoesNotContain("ignoredroute", helper.Types);
            Assert.Equal(1, helper.Types.Count);
        }
        
        [Fact]
        public void ShouldPopulateWithHelperTypes()
        {
            var provider = new ServiceCollection();
            provider.UseSharpArgs<IRoute>();
            Assert.Contains(provider, x => x.ServiceType == typeof(ITypeHelper<IRoute>));
            Assert.Contains(provider, x => x.ServiceType == typeof(ICliParser<IRoute>));
            Assert.Contains(provider, x => x.ServiceType == typeof(IRouter<IRoute>));
        }
    }
}