using RoseByte.SharpArgs.Internal.Parser.Options;
using RoseByte.SharpArgs.Internal.Parser.Parts;
using Xunit;

namespace RoseByte.SharpArgs.Tests.Internal.Parser
{
    public class PartTest
    {
        [Fact]
        public void ShouldReturnContent()
        {
            var sut = new Part("--name:content", new ParsingOptions());
            
            Assert.Equal("--name:content", sut.Content);
        }
        
        [Fact]
        public void ShouldReturnValueWithDelimiter()
        {
            var sut = new Part("--name:content", new ParsingOptions
            {
                OptionPrefix = "--",
                Delimiters = {':'}
            });
            
            Assert.Equal("content", sut.Value);
        }
        
        [Fact]
        public void ShouldReturnLabelWithDelimiter()
        {
            var sut = new Part("--name:content", new ParsingOptions
            {
                OptionPrefix = "--",
                Delimiters = {':'}
            });
            
            Assert.Equal("name", sut.Label);
        }
        
        [Fact]
        public void ShouldReturnValueWithoutDelimiter()
        {
            var sut = new Part("content:this", new ParsingOptions
            {
                OptionPrefix = "--",
                Delimiters = {':'}
            });
            
            Assert.Equal("content:this", sut.Value);
        }
        
        [Fact]
        public void ShouldReturnLabelWithoutDelimiter()
        {
            var sut = new Part("--name", new ParsingOptions
            {
                OptionPrefix = "--",
                Delimiters = {':'}
            });
            
            Assert.Equal("name", sut.Label);
        }
        
        [Fact]
        public void ShouldReturnFlag()
        {
            var sut = new Part("--n", new ParsingOptions
            {
                FlagPrefix = "--",
                OptionPrefix = "-"
            });
            Assert.True(sut.IsFlag);
            Assert.False(sut.IsOption);
        }
        
        [Fact]
        public void ShouldReturnOption()
        {
            var sut = new Part("--", new ParsingOptions());
            Assert.True(sut.IsOption);
            Assert.False(sut.IsFlag);
        }
    }
}