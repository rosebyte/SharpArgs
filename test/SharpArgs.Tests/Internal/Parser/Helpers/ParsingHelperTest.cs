using Moq;
using RoseByte.SharpArgs.Internal.Parser.Helpers;
using RoseByte.SharpArgs.Internal.Properties;
using Xunit;

namespace SharpArgs.Internal.Parser.Helpers
{
    public class ParsingHelperTest
    {
        [Fact]
        public void ShouldRegisterProperties()
        {
            var property = new Mock<IProperty>();
            property.Setup(x => x.Order).Returns(7);
            property.Setup(x => x.Shortcut).Returns('t');
            property.Setup(x => x.Label).Returns("test-label");
            var sut = new ParsingHelper(new []{property.Object});

            Assert.Contains(sut.Positions, x => x.Key == 7 && x.Value == property);
            Assert.Contains(sut.Shortcuts, x => x.Key == 't' && x.Value == property);
            Assert.Contains(sut.Labels, x => x.Key == "test-label" && x.Value == property);
        }
    }
}