using System.Linq;
using RoseByte.SharpArgs.Attributes;
using RoseByte.SharpArgs.Internal.Extensions;
using RoseByte.SharpArgs.Tests.TestObjects;
using Xunit;

namespace RoseByte.SharpArgs.Tests.Internal.Extensions
{
    public class PropertyInfoExtensionsTest
    {
        [Fact]
        public void ShouldGetAttribute()
        {
            var sut = typeof(TestRoute).GetProperties().FirstOrDefault(x => x.Name == "IgnoredProp");
            var attribute = sut?.GetCustomAttributes(true)
                .FirstOrDefault(x => x.GetType() == typeof(IgnoreAttribute));
            
            Assert.NotNull(attribute);

            var result = sut.GetAttribute<IgnoreAttribute>();
            
            Assert.Equal(attribute, result);
        }
        
        [Fact]
        public void ShouldReturnNullWhenPropertyDoesntHaveAttribute()
        {
            var sut = typeof(TestRoute).GetProperties().FirstOrDefault(x => x.Name == "IntProp");
            
            Assert.NotNull(sut);

            var result = sut.GetAttribute<IgnoreAttribute>();
            
            Assert.Null(result);
        }
        
        [Fact]
        public void ShouldTestAttribute()
        {
            var sut = typeof(TestRoute).GetProperties().FirstOrDefault(x => x.Name == "IgnoredProp");
            Assert.NotNull(sut);
            Assert.True(sut.HasAttribute<IgnoreAttribute>());
        }
        
        [Fact]
        public void ShouldTestMissingAttribute()
        {
            var sut = typeof(TestRoute).GetProperties().FirstOrDefault(x => x.Name == "IntProp");
            Assert.NotNull(sut);
            Assert.False(sut.HasAttribute<IgnoreAttribute>());
        }
    }
}