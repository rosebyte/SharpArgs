using Microsoft.Extensions.DependencyInjection;
using RoseByte.SharpArgs.Internal.Routers;
using RoseByte.SharpArgs.Tests.TestObjects;
using Xunit;

namespace RoseByte.SharpArgs.Tests.Internal.Routers
{
    public class RouterTest
    {
        [Fact]
        public void ShouldFindRoute()
        {
            var provider = new ServiceCollection().UseSharpArgs<ISecondRoute>().BuildServiceProvider();
            var sut = new Router<ISecondRoute>(provider);

            var result = sut.Resolve(new[] {"first", "second"});
            
            Assert.True(result.Success);
            Assert.Equal(result.Provider, provider);
            Assert.IsType<WithRouteAttribute>(result.Route);
            Assert.Collection(
                result.OriginalArgs, 
                x => Assert.Equal("first", x), 
                x => Assert.Equal("second", x));
            Assert.Collection(
                result.CurrentArgs, 
                x => Assert.Equal("second", x));
        }
        
        [Fact]
        public void ShouldNotFindRoute()
        {
            var provider = new ServiceCollection().UseSharpArgs<ISecondRoute>().BuildServiceProvider();
            var sut = new Router<ISecondRoute>(provider);

            var result = sut.Resolve(new[] {"third", "second"});
            
            Assert.False(result.Success);
            Assert.Equal(result.Provider, provider);
            Assert.Null(result.Route);
            Assert.Collection(
                result.OriginalArgs, 
                x => Assert.Equal("third", x), 
                x => Assert.Equal("second", x));
            Assert.Collection(
                result.CurrentArgs,
                x => Assert.Equal("third", x), 
                x => Assert.Equal("second", x));
        }
    }
}