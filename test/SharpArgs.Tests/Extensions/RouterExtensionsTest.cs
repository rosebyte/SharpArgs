using System.Collections.Generic;
using Moq;
using RoseByte.SharpArgs.Exceptions;
using RoseByte.SharpArgs.Internal.Parser;
using RoseByte.SharpArgs.Internal.Properties;
using Xunit;

namespace RoseByte.SharpArgs.Tests.Extensions
{
    public class RouterExtensionsTest
    {
        [Fact]
        public void ShouldRunRoute()
        {
            var properties = new List<Property>{};
            var options = new ParsingOptions();
            var route = new Mock<IRoute>();
            var robject = route.Object;
            
            var parser = new Mock<ICliParser>();
            parser.Setup(x => x.Scan(properties)).Returns(parser.Object);
            
            var defaultUsed = false;
            
            var router = new Mock<IRouter>();
            router.Setup(x => x.GetParser()).Returns(parser.Object);
            router.Setup(x => x.Options).Returns(options);
            router.Setup(x => x.TryGetRoute("first", out robject, out defaultUsed)).Returns(true);
            
            router.Object.Run(new[] {"first", "second"});
            
            parser.Verify(x => x.Parse(new []{"second"}, options), Times.Once);
            route.Verify(x => x.Execute(), Times.Once);
        }
        
        [Fact]
        public void ShouldThrowOnUnknownRoute()
        {
            IRoute route = default;
            var defaultUsed = false;
            
            var router = new Mock<IRouter>();
            router.Setup(x => x.TryGetRoute("first", out route, out defaultUsed)).Returns(false);
            
            Assert.Throws<SharpArgsException>(() => router.Object.Run(new[] {"first", "second"}));
        }
    }
}