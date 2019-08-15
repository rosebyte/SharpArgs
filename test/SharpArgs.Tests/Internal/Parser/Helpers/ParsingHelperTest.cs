using Moq;
using RoseByte.SharpArgs.Internal.Parser.Helpers;
using RoseByte.SharpArgs.Internal.Properties;
using RoseByte.SharpArgs.Tests.TestObjects;
using Xunit;

namespace RoseByte.SharpArgs.Tests.Internal.Parser.Helpers
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

            Assert.Contains(sut.Positions, x => x.Key == 7 && x.Value == property.Object);
            Assert.Contains(sut.Shortcuts, x => x.Key == 't' && x.Value == property.Object);
            Assert.Contains(sut.Labels, x => x.Key == "test-label" && x.Value == property.Object);
        }
        
        [Fact]
        public void ShouldRegisterFromObject()
        {
            var sut = new ParsingHelper(new PropertyTestObject());
            
            Assert.Contains(sut.Labels, x => x.Key == nameof(PropertyTestObject.BoolProp).ToLower());
            Assert.Contains(sut.Labels, x => x.Key == nameof(PropertyTestObject.StrProp).ToLower());
            Assert.Contains(sut.Labels, x => x.Key == nameof(PropertyTestObject.IntProp).ToLower());
            Assert.Contains(sut.Labels, x => x.Key == nameof(PropertyTestObject.DateTimeProp).ToLower());
            Assert.Contains(sut.Labels, x => x.Key == nameof(PropertyTestObject.ShortcutProp).ToLower());
            Assert.Contains(sut.Labels, x => x.Key == "testme");
            Assert.Contains(sut.Labels, x => x.Key == nameof(PropertyTestObject.OrderedProp).ToLower());
            Assert.Contains(sut.Labels, x => x.Key == nameof(PropertyTestObject.DescribedProp).ToLower());
            Assert.Equal(8, sut.Labels.Count);
                
            Assert.Contains(sut.Positions, x => x.Key == 4);
            Assert.Single(sut.Positions);
                
            Assert.Contains(sut.Shortcuts, x => x.Key == 's');
            Assert.Single(sut.Shortcuts);
        }
    }
}