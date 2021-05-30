using System.Collections.Generic;
using RoseByte.SharpArgs;
using RoseByte.SharpArgs.Parsers;
using Xunit;

namespace SharpArgs.Tests
{
    public class UnixArgumentParserTest
    {
        [Fact]
        public void ThrowOnUnbindableParameter()
        {
            var sut = Parser.Unix;
            Assert.Throws<SharpArgsException>(() => sut.ParseArgs(new[] {"--foo", "bar"}, new List<Argument>()));
            Assert.Throws<SharpArgsException>(() => sut.ParseArgs(new[] {"-f", "bar"}, new List<Argument>()));
        }
        
        [Fact]
        public void ThrowOnUnbindableOption()
        {
            var sut = Parser.Unix;
            Assert.Throws<SharpArgsException>(() => sut.ParseArgs(new[] {"bar"}, new List<Argument>()));
        }

        [Fact]
        public void ThrowOnUnbindableFlag()
        {
            var sut = Parser.Unix;
            Assert.Throws<SharpArgsException>(() => sut.ParseArgs(new[] {"-f"}, new List<Argument>()));
        }
        
        [Fact]
        public void ThrowOnUnbindableGroup()
        {
            var sut = Parser.Unix;
            Assert.Throws<SharpArgsException>(() => sut.ParseArgs(new[] {"-bar"}, new List<Argument>()));
        }
        
        [Fact]
        public void BindParameter()
        {
            var parameter = new Parameter<int>("foo", string.Empty);
            
            var sut = Parser.Unix;
            sut.ParseArgs(new []{"--foo", "15"}, new List<Argument>{ parameter });
            
            Assert.Equal(15, parameter.Value);
            Assert.True(parameter.Initialized);
        }
        
        [Fact]
        public void BindParameterByShortname()
        {
            var parameter = new Parameter<int>(string.Empty, string.Empty, 'f');
            
            var sut = Parser.Unix;
            sut.ParseArgs(new []{"-f", "15"}, new List<Argument>{ parameter });
            
            Assert.Equal(15, parameter.Value);
            Assert.True(parameter.Initialized);
        }
        
        [Fact]
        public void BindFlag()
        {
            var parameter = new Flag("foo", string.Empty);
            
            var sut = Parser.Unix;
            sut.ParseArgs(new []{"--foo"}, new List<Argument>{ parameter });
            
            Assert.True(parameter.Value);
            Assert.True(parameter.Initialized);
        }
        
        [Fact]
        public void BindFlagWithExplicitValue()
        {
            var parameter = new Flag("foo", string.Empty);
            
            var sut = Parser.Unix;
            sut.ParseArgs(new []{"--foo", "true"}, new List<Argument>{ parameter });
            
            Assert.True(parameter.Value);
            Assert.True(parameter.Initialized);
        }

        [Fact]
        public void BindFlagByShortname()
        {
            var parameter = new Flag(string.Empty, string.Empty, 'f');
            
            var sut = Parser.Unix;
            sut.ParseArgs(new []{"-f"}, new List<Argument>{ parameter });
            
            Assert.True(parameter.Value);
            Assert.True(parameter.Initialized);
        }
        
        [Fact]
        public void BindFlagByShortnameAndExplicitName()
        {
            var parameter = new Flag(string.Empty, string.Empty, 'f');
            
            var sut = Parser.Unix;
            sut.ParseArgs(new []{"-f", "true"}, new List<Argument>{ parameter });
            
            Assert.True(parameter.Value);
            Assert.True(parameter.Initialized);
        }
        
        [Fact]
        public void BindGroup()
        {
            var parameter1 = new Flag(string.Empty, string.Empty, 'f');
            var parameter2 = new Flag(string.Empty, string.Empty, 'g');
            var parameter3 = new Flag(string.Empty, string.Empty, 'h');
            
            var sut = Parser.Unix;
            sut.ParseArgs(new []{"-fgh"}, new List<Argument>{ parameter1, parameter2, parameter3 });
            
            Assert.True(parameter1.Value);
            Assert.True(parameter1.Initialized);
            Assert.True(parameter2.Value);
            Assert.True(parameter2.Initialized);
            Assert.True(parameter3.Value);
            Assert.True(parameter3.Initialized);
        }
        
        [Fact]
        public void BindOption()
        {
            var parameter1 = new Option<int>(1, string.Empty);
            var parameter2 = new Option<int>(2, string.Empty);
            var parameter3 = new Option<int>(3, string.Empty);
            
            var sut = Parser.Unix;
            sut.ParseArgs(new []{"10", "20", "30"}, new List<Argument>{ parameter1, parameter2, parameter3 });
            
            Assert.Equal(10, parameter1.Value);
            Assert.True(parameter1.Initialized);
            Assert.Equal(20, parameter2.Value);
            Assert.True(parameter2.Initialized);
            Assert.Equal(30, parameter3.Value);
            Assert.True(parameter3.Initialized);
        }
    }
}