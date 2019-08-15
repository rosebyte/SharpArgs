using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using RoseByte.SharpArgs.Internal.Parser;
using RoseByte.SharpArgs.Internal.Parser.Options;
using RoseByte.SharpArgs.Internal.Properties;
using RoseByte.SharpArgs.Tests.TestObjects;
using Xunit;

namespace RoseByte.SharpArgs.Tests.Extensions
{
    public class RouterExtensionsTest
    {
        [Fact]
        public void ShouldBind()
        {
            var parser = new Mock<ICliParser<IRoute>>();
            var args = new string[]{};
            var route = new TestRoute();
            var provider = new Mock<IServiceProvider>();
            provider.Setup(x => x.GetService(typeof(ICliParser<IRoute>))).Returns(parser.Object);
            var result = new Mock<IResult<IRoute>>();
            result.Setup(x => x.Provider).Returns(provider.Object);
            result.Setup(x => x.Success).Returns(true);
            result.Setup(x => x.CurrentArgs).Returns(args);
            result.Setup(x => x.Route).Returns(route);
            var options = new Mock<IParsingOptions>();

            result.Object.Bind(options.Object);
            
            parser.Verify(x => x.Register(result.Object));
            parser.Verify(x => x.Parse(args, options.Object), Times.Once);
        }
        
        [Fact]
        public void ShouldNotBindOnFailure()
        {
            var result = new Mock<IResult<IRoute>>();
            result.Setup(x => x.Success).Returns(false);
            var options = new Mock<IParsingOptions>();

            result.Object.Bind(options.Object);
            
            result.Verify(x => x.Provider, Times.Never);
        }
    }
}