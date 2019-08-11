using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using RoseByte.SharpArgs.Internal.Parser;
using RoseByte.SharpArgs.Internal.Properties;
using RoseByte.SharpArgs.Tests.TestObjects;
using SharpArgs.TestObjects;
using Xunit;

namespace RoseByte.SharpArgs.Tests.Extensions
{
    public class RouterExtensionsTest
    {
        [Fact]
        public void ShouldBind()
        {
            var parser = new Mock<ICliParser>();
            var args = new string[]{};
            var route = new TestRoute();
            var provider = new Mock<IServiceProvider>();
            provider.Setup(x => x.GetService(typeof(ICliParser))).Returns(parser.Object);
            var result = new Mock<IResult<IRoute>>();
            result.Setup(x => x.Provider).Returns(provider.Object);
            result.Setup(x => x.Success).Returns(true);
            result.Setup(x => x.CurrentArgs).Returns(args);
            result.Setup(x => x.Route).Returns(route);
            var options = new Mock<IReadOnlyParsingOptions>();
            var properties = Internal.Extensions.TypeExtensions.ExtractProperties(route);

            result.Object.Bind(options.Object);
            
            parser.Verify(x => x.Scan(
                It.Is<IEnumerable<Property>>(y => y.All(z => properties.Any(a => a == z)))), 
                Times.Once);
            parser.Verify(x => x.Parse(args, options.Object), Times.Once);
        }
        
        [Fact]
        public void ShouldNotBindOnFailure()
        {
            var result = new Mock<IResult<IRoute>>();
            result.Setup(x => x.Success).Returns(false);
            var options = new Mock<IReadOnlyParsingOptions>();

            result.Object.Bind(options.Object);
            
            result.Verify(x => x.Provider, Times.Never);
        }
    }
}