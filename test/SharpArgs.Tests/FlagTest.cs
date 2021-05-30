using RoseByte.SharpArgs;
using Xunit;

namespace SharpArgs.Tests
{
    public class FlagTest
    {
        [Fact]
        public void ConstructInstance()
        {
            var description = "this is a description";
            var sut = new Flag("test", description, 't', false, true);
            
            Assert.Equal("test", sut.Name);
            Assert.Equal('t', sut.Shortcut);
            Assert.Equal(description, sut.Description);
            Assert.False(sut.Combinable);
            Assert.True(sut.DefaultValue);
        }
        
        [Fact]
        public void SetDefaultValues()
        {
            var sut = new Flag(default, default);
            
            Assert.Equal(default, sut.Name);
            Assert.Equal(default, sut.Shortcut);
            Assert.Equal(default, sut.Description);
            Assert.True(sut.Combinable);
            Assert.False(sut.DefaultValue);
        }
    }
}