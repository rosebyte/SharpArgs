using System;
using Microsoft.Extensions.DependencyInjection;
using RoseByte.SharpArgs;
using RoseByte.SharpArgs.Internal.Parser;
using RoseByte.SharpArgs.Internal.Parser.Options;
using RoseByte.SharpArgs.Tests.TestObjects;
using SharpArgs.TestObjects;
using Xunit;

namespace SharpArgs
{
    public class CliTest
    {
        [Fact]
        public void ShouldRunParser()
        {
            var provider = new ServiceCollection().UseSharpArgs<IRoute>(typeof(IRoute).Assembly);
            var result = Cli.Router<IRoute>(provider.BuildServiceProvider())
                .Resolve(
                    new[]
                    {
                        "bindingtest", "first", "second", "-f", "-g:true", "--FlagOpt", "--Option", "value 1", 
                        "--optionVal:value 2"
                    });

            Assert.True(result.Success);

            result.Bind(new ParsingOptions{ Delimiters = new []{':', ' '}});
            
            Assert.IsType<BindingTest>(result.Route);

            var rs = result.Route as BindingTest;
            
            Assert.True(rs.Flag);
            Assert.True(rs.FlagVal);
            Assert.True(rs.FlagOpt);
            Assert.Equal("value 1", rs.Option);
            Assert.Equal("value 2", rs.OptionVal);
            Assert.Equal("value 2", rs.OptionVal);
            Assert.Equal("first", rs.First);
            Assert.Equal("second", rs.Second);
        }
    }
}