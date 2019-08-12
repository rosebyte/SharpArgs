using System;
using System.Linq;
using System.Reflection;
using RoseByte.SharpArgs.Attributes;
using RoseByte.SharpArgs.Internal.Extensions;
using RoseByte.SharpArgs.Tests.TestObjects;
using Xunit;

namespace RoseByte.SharpArgs.Tests.Extensions
{
    public class TypeExtensions
    {
        [Theory]
        [InlineData(typeof(IgnoredRoute), true)]
        [InlineData(typeof(TestRoute), false)]
        public void ShouldTestIfRouteHasAttribute(Type type, bool expectation)
        {
            Assert.Equal(expectation, type.HasAttribute<IgnoreAttribute>());
        }
        
        [Theory]
        [InlineData(typeof(IgnoredRoute), true)]
        [InlineData(typeof(TestRoute), false)]
        public void ShouldGetAttribute(Type type, bool hasAttribute)
        {
            var attribute = type.GetAttribute<IgnoreAttribute>();

            if (hasAttribute)
            {
                Assert.NotNull(attribute);
                Assert.IsType<IgnoreAttribute>(attribute);
            }
            else
            {
                Assert.Null(attribute);
            }
        }
        
        [Fact]
        public void ShouldExtractProperties()
        {
            var properties = Internal.Extensions.TypeExtensions
                .ExtractProperties(new TestRoute())
                .ToList();
            
            Assert.Equal(3, properties.Count);
            Assert.Contains(properties, x => x.Name == "stringprop");
            Assert.Contains(properties, x => x.Name == "intprop");
            Assert.Contains(properties, x => x.Name == "boolprop");
            Assert.DoesNotContain(properties, x => x.Name == "ignoredprop");
        }
    }
}