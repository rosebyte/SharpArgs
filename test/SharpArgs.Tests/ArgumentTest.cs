using SharpArgs.Tests.TestObjects;
using Xunit;

namespace SharpArgs.Tests
{
    public class ArgumentTest
    {
        [Fact]
        public void InitializeByName()
        {
            var sut = new DummyIntArgument("foo");

            Assert.True(sut.Initialize("foo", "10"));
            Assert.Equal(10, sut.Value);
            Assert.True(sut.Initialized);
        }
        
        [Fact]
        public void DoNotInitializeByNameWhenItDiffers()
        {
            var sut = new DummyIntArgument("foo");

            Assert.False(sut.Initialize("bar", "10"));
            Assert.Equal(0, sut.Value);
            Assert.False(sut.Initialized);
        }

        [Fact]
        public void DoNotInitializeTwiceByName()
        {
            var sut = new DummyIntArgument("foo");

            Assert.True(sut.Initialize("foo", "10"));
            Assert.Equal(10, sut.Value);
            Assert.True(sut.Initialized);
            Assert.False(sut.Initialize("foo", "10"));
        }
        
        [Fact]
        public void InitializeByShortcut()
        {
            var sut = new DummyIntArgument('f');

            Assert.True(sut.Initialize('f', "10"));
            Assert.Equal(10, sut.Value);
            Assert.True(sut.Initialized);
        }
        
        [Fact]
        public void DoNotInitializeByShortcutWhenItDiffers()
        {
            var sut = new DummyIntArgument('f');

            Assert.False(sut.Initialize('b', "10"));
            Assert.Equal(0, sut.Value);
            Assert.False(sut.Initialized);
        }

        [Fact]
        public void DoNotInitializeTwiceByShortcut()
        {
            var sut = new DummyIntArgument('f');

            Assert.True(sut.Initialize('f', "10"));
            Assert.Equal(10, sut.Value);
            Assert.True(sut.Initialized);
            Assert.False(sut.Initialize('f', "10"));
        }
        
        [Fact]
        public void InitializeByOrder()
        {
            var sut = new DummyIntArgument(5);

            Assert.True(sut.Initialize(5, "10"));
            Assert.Equal(10, sut.Value);
            Assert.True(sut.Initialized);
        }
        
        [Fact]
        public void DoNotInitializeByOrderWhenItDiffers()
        {
            var sut = new DummyIntArgument(5);

            Assert.False(sut.Initialize(6, "10"));
            Assert.Equal(0, sut.Value);
            Assert.False(sut.Initialized);
        }

        [Fact]
        public void DoNotInitializeTwiceByOrder()
        {
            var sut = new DummyIntArgument(5);

            Assert.True(sut.Initialize(5, "10"));
            Assert.Equal(10, sut.Value);
            Assert.True(sut.Initialized);
            Assert.False(sut.Initialize(5, "10"));
        }
        
        [Fact]
        public void ValidateIfInitialized()
        {
            var firstValidation = false;
            var secondValidation = false;
            
            var sut = new DummyBoolArgument("test");
            sut.ValidationAction = () => firstValidation = true;
            sut.Initialize("test", "true");
            sut.ValidationAction = () => secondValidation = true;
            sut.Initialize("test", "true");

            Assert.True(firstValidation);
            Assert.False(secondValidation);
        }
        
        [Fact]
        public void InitializeBoolArgument()
        {
            var sut = new DummyBoolArgument("test");
            Assert.True(sut.Initialize("test", "true"));
            Assert.True(sut.Value);
            
            sut = new DummyBoolArgument("test");
            Assert.True(sut.Initialize("test", "false"));
            Assert.False(sut.Value);
        }
        
        [Fact]
        public void InitializeStringArgument()
        {
            var sut = new DummyStringArgument("test");
            Assert.True(sut.Initialize("test", "true"));
            Assert.Equal("true", sut.Value);
        }
        
        [Fact]
        public void InitializeIntArgument()
        {
            var sut = new DummyIntArgument("test");
            Assert.True(sut.Initialize("test", "10"));
            Assert.Equal(10, sut.Value);
        }
        
        [Fact]
        public void InitializeDecimalArgument()
        {
            var sut = new DummyDecimalArgument("test");
            Assert.True(sut.Initialize("test", "10,5"));
            Assert.Equal(10.5M, sut.Value);
        }
    }
}