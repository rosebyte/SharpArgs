using RoseByte.SharpArgs;
using Xunit;

namespace SharpArgs.Tests
{
    public class OptionTest
    {
        [Fact]
        public void ConstructInstance()
        {
            var description = "this is a description";
            var sut = new Option<int>(5, description, 25);
            
            Assert.Equal(description, sut.Description);
            Assert.Equal(5, sut.Order);
            Assert.Equal(25, sut.DefaultValue);
        }
        
        [Fact]
        public void ConstructInstanceWithDefaults()
        {
            var description = "this is a description";
            var sut = new Option<int>(5, description);
            
            Assert.Equal(description, sut.Description);
            Assert.Equal(5, sut.Order);
            Assert.Equal(0, sut.DefaultValue);
        }
    }
}