using RoseByte.SharpArgs;
using Xunit;

namespace SharpArgs.Tests
{
    public class ParameterTest
    {
        [Fact]
        public void ConstructInstance()
        {
            var description = "this is a description";
            var sut = new Parameter<int>("test", description, 't', true, 55);
            
            Assert.Equal("test", sut.Name);
            Assert.Equal('t', sut.Shortcut);
            Assert.Equal(description, sut.Description);
            Assert.False(sut.Combinable);
            Assert.Equal(55, sut.DefaultValue);
        }
        
        [Fact]
        public void SetDefaultValues()
        {
            var sut = new Parameter<int>(default, default);
            
            Assert.Equal(default, sut.Name);
            Assert.Equal(default, sut.Shortcut);
            Assert.Equal(default, sut.Description);
            Assert.False(sut.Combinable);
            Assert.Equal(0, sut.DefaultValue);
        }
    }
}