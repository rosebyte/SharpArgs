using System.Collections.Generic;
using Moq;
using RoseByte.SharpArgs.Internal.Parser;
using RoseByte.SharpArgs.Internal.Parser.Helpers;
using RoseByte.SharpArgs.Internal.Properties;
using Xunit;

namespace RoseByte.SharpArgs.Tests.Internal.Parser
{
    public class ArgumentsParserTest
    {
        [Fact]
        public void ShouldParse()
        {
            var mock1 = new Mock<IProperty>();
            var mock2 = new Mock<IProperty>();
            var mock3 = new Mock<IProperty>();
            var mock4 = new Mock<IProperty>();
            
            var positions = new Dictionary<int, IProperty>
            {
                {1, mock1.Object},
                {2, mock2.Object},
                {4, mock3.Object},
                {5, mock4.Object}
            };
            var resolved = new HashSet<IProperty>{mock2.Object};
            
            var helper = new Mock<IParsingHelper>();
            helper.Setup(x => x.Positions).Returns(positions);
            helper.Setup(x => x.Resolved).Returns(resolved);

            var sut = new ArgumentsParser();
            sut.ParseArgs(new []{"first", "second"}, helper.Object);
            
            mock1.Verify(x => x.Set("first"), Times.Once);
            mock2.Verify(x => x.Set(It.IsAny<string>()), Times.Never);
            mock2.Verify(x => x.Set(It.IsAny<bool>()), Times.Never);
            mock3.Verify(x => x.Set("second"), Times.Once);
            mock4.Verify(x => x.Set(It.IsAny<string>()), Times.Never);
            mock4.Verify(x => x.Set(It.IsAny<bool>()), Times.Never);

            Assert.Contains(resolved, x => x == mock1.Object);
            Assert.Contains(resolved, x => x == mock2.Object);
            Assert.Contains(resolved, x => x == mock3.Object);
        }
    }
}