using System;
using System.Collections.Generic;
using Moq;
using RoseByte.SharpArgs.Internal.Parser;
using RoseByte.SharpArgs.Internal.Parser.Helpers;
using RoseByte.SharpArgs.Internal.Parser.Options;
using RoseByte.SharpArgs.Tests.TestObjects;
using SharpArgs.TestObjects;
using Xunit;

namespace RoseByte.SharpArgs.Tests.Internal.Parser
{
    public class CliParserTest
    {
        [Fact]
        public void ShouldParse()
        {
            var route = new TestRoute();
            var helper = new Mock<IParsingHelper>();
            var argParser = new Mock<IArgumentsParser>();
            var optParser = new Mock<IOptionsParser>();
            var factory = new Mock<IParsingHelperFactory>();
            factory.Setup(x => x.Create(route)).Returns(helper.Object);
            var provider = new Mock<IServiceProvider>();
            provider.Setup(x => x.GetService(typeof(IParsingHelperFactory))).Returns(factory.Object);
            provider.Setup(x => x.GetService(typeof(IArgumentsParser))).Returns(argParser.Object);
            provider.Setup(x => x.GetService(typeof(IOptionsParser))).Returns(optParser.Object);
            var args = new[] {"first", "second", "-f", "--name", "example"};
            
            var result = new Result<IRoute>
            {
                Route = route,
                Provider = provider.Object
            };

            var sut = new CliParser<IRoute>();
            sut.Register(result);
            var options = new ParsingOptions();
            sut.Parse(args, options);
            
            optParser.Verify(x => x.ParseParams(
                It.Is<IReadOnlyList<string>>(y => string.Join(" ", y) == "-f --name example"),
                options,
                helper.Object));
            argParser.Verify(x => x.ParseArgs(
                It.Is<IReadOnlyList<string>>(y => string.Join(" ", y) == "first second"),
                helper.Object));
        }
    }
}