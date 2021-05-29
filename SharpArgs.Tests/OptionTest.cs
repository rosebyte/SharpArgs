using RoseByte.SharpArgs;
using RoseByte.SharpArgs.Options;
using Xunit;

namespace SharpArgs.Tests
{
    public class OptionTest
    {
        [Fact]
        public void ConstructInstance()
        {
            var description = "this is a description";
            var sut = new Option<int>(5, description);
            
            Assert.Equal(description, sut.Description);
            Assert.Equal(5, sut.Order);
        }
    }
}