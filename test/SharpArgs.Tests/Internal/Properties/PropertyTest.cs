using System;
using System.Linq;
using RoseByte.SharpArgs.Internal.Extensions;
using RoseByte.SharpArgs.Internal.Properties;
using RoseByte.SharpArgs.Tests.TestObjects;
using Xunit;

namespace RoseByte.SharpArgs.Tests.Internal.Properties
{
    public class PropertyTest
    {
        [Fact]
        public void ShouldReadLabel()
        {
            var property = new PropertyTestObject().ExtractProperties()
                .FirstOrDefault(x => x.Name == nameof(PropertyTestObject.LabeledProp).ToLower());
            Assert.NotNull(property);
            Assert.Equal("testme", property.Label);
        }
        
        [Fact]
        public void ShouldReadOrder()
        {
            var property = new PropertyTestObject().ExtractProperties()
                .FirstOrDefault(x => x.Name == nameof(PropertyTestObject.OrderedProp).ToLower());
            Assert.NotNull(property);
            Assert.Equal(4, property.Order);
        }
        
        [Fact]
        public void ShouldReadShortcut()
        {
            var property = new PropertyTestObject().ExtractProperties()
                .FirstOrDefault(x => x.Name == nameof(PropertyTestObject.ShortcutProp).ToLower());
            Assert.NotNull(property);
            Assert.Equal('s', property.Shortcut);
        }
        
        [Fact]
        public void ShouldReadDescription()
        {
            var property = new PropertyTestObject().ExtractProperties()
                .FirstOrDefault(x => x.Name == nameof(PropertyTestObject.DescribedProp).ToLower());
            Assert.NotNull(property);
            Assert.Equal("This is a short description.", property.Description);
        }
        
        [Fact]
        public void ShouldReadName()
        {
            var instance = new PropertyTestObject();
            var property = instance.GetType().GetProperties()
                .FirstOrDefault(x => x.Name == nameof(PropertyTestObject.DescribedProp));
            Assert.NotNull(property);
            var sut = new Property(instance, property);
            Assert.Equal(nameof(PropertyTestObject.DescribedProp).ToLower(), sut.Name);
        }
        
        [Fact]
        public void ShouldReadType()
        {
            var property = new PropertyTestObject().ExtractProperties()
                .FirstOrDefault(x => x.Name == nameof(PropertyTestObject.IntProp).ToLower());
            Assert.NotNull(property);
            Assert.Equal(typeof(int), property.Type);
        }
        
        [Fact]
        public void ShouldWriteBool()
        {
            var instance = new PropertyTestObject();
            var property = instance.ExtractProperties()
                .FirstOrDefault(x => x.Name == nameof(PropertyTestObject.BoolProp).ToLower());
            Assert.NotNull(property);
            Assert.False(instance.BoolProp);
            property.Set(true);
            Assert.True(instance.BoolProp);
            property.Set(false);
            Assert.False(instance.BoolProp);
        }
        
        [Fact]
        public void ShouldWriteString()
        {
            var instance = new PropertyTestObject();
            var property = instance.ExtractProperties()
                .FirstOrDefault(x => x.Name == nameof(PropertyTestObject.StrProp).ToLower());
            Assert.NotNull(property);
            Assert.Null(instance.StrProp);
            property.Set("test");
            Assert.Equal("test", instance.StrProp);
        }
        
        [Fact]
        public void ShouldWriteInt()
        {
            var instance = new PropertyTestObject();
            var property = instance.ExtractProperties()
                .FirstOrDefault(x => x.Name == nameof(PropertyTestObject.IntProp).ToLower());
            Assert.NotNull(property);
            Assert.Equal(0, instance.IntProp);
            property.Set("3");
            Assert.Equal(3, instance.IntProp);
        }
        
        [Fact]
        public void ShouldWriteDate()
        {
            var instance = new PropertyTestObject();
            var property = instance.ExtractProperties()
                .FirstOrDefault(x => x.Name == nameof(PropertyTestObject.DateTimeProp).ToLower());
            Assert.NotNull(property);
            Assert.Equal(default, instance.DateTimeProp);
            property.Set("2019-12-13");
            Assert.Equal(new DateTime(2019, 12, 13), instance.DateTimeProp);
        }
        
        [Fact]
        public void ShouldReadDefaultLabel()
        {
            var property = new PropertyTestObject().ExtractProperties()
                .FirstOrDefault(x => x.Name == nameof(PropertyTestObject.StrProp).ToLower());
            Assert.NotNull(property);
            Assert.Equal("strprop", property.Label);
        }
    }
}