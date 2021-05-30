using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using RoseByte.SharpArgs;
using RoseByte.SharpArgs.Parsers;
using SharpArgs.Tests.TestObjects.Commands;
using Xunit;

namespace SharpArgs.Tests
{
    public class CommandTest
    {
        [Fact]
        public void SelectFirstCommand()
        {
            var result = "";

            var sut = new RootCommand(x => result = x);
            sut.Run("first");
            
            Assert.Equal("first", result);
        }
        
        [Fact]
        public void SelectSecondCommand()
        {
            var result = "";

            var sut = new RootCommand(x => result = x);
            sut.Run("first");
            
            Assert.Equal("first", result);
        }
        
        [Fact]
        public void SelectNoCommand()
        {
            var result = "";

            var sut = new RootCommand(x => result = x);
            sut.Run(new Mock<IArgumentsParser>().Object, "third");
            
            Assert.Equal("", result);
        }
        
        [Fact]
        public void RunCommand()
        {
            var result = false;

            var sut = new FirstCommand(() => result = true);
            sut.Run();
            
            Assert.True(result);
        }
        
        [Fact]
        public async Task RunAsyncCommand()
        {
            var result = false;

            var sut = new AsyncCommand(async () => result = true);
            await sut.RunAsync();
            
            Assert.True(result);
        }
        
        [Fact]
        public void ValidateBeforeRun()
        {
            var result = false;

            var sut = new ValidationCommand(() => result = true);
            sut.RunAsync();
            
            Assert.True(result);
        }
        
        [Fact]
        public async Task ValidateBeforeRunAsync()
        {
            var result = false;

            var sut = new ValidationCommand(() => result = true);
            await sut.RunAsync();
            
            Assert.True(result);
        }
        
        [Fact]
        public async Task UseParserForRun()
        {
            var flag = new Flag("test", "this is test flag");
            var option = new Option<bool>(1, "this is test flag");
            var args = new[] {"-a", "b"};
            var parserMock = new Mock<IArgumentsParser>(MockBehavior.Strict);
            parserMock.Setup(x => x.ParseArgs(
                args,
                It.Is<List<Argument>>(y => y.Count == 2 && y[0] == flag && y[1] == option)));

            var sut = new ParserCommand(flag, option);
            await sut.RunAsync(parserMock.Object, args);
        }
        
        [Fact]
        public async Task UseParserForRunAsync()
        {
            var flag = new Flag("test", "this is test flag");
            var option = new Option<bool>(1, "this is test flag");
            var args = new[] {"-a", "b"};
            var parserMock = new Mock<IArgumentsParser>(MockBehavior.Strict);
            parserMock.Setup(x => x.ParseArgs(
                args,
                It.Is<List<Argument>>(y => y.Count == 2 && y[0] == flag && y[1] == option)));

            var sut = new ParserCommand(flag, option);
            await sut.RunAsync(parserMock.Object,args);
        }
        
        [Fact]
        public void PrepareArguments()
        {
            var arguments = new List<Argument>();
            var parserMock = new Mock<IArgumentsParser>(MockBehavior.Strict);
            parserMock.Setup(x => x.ParseArgs(It.IsAny<IReadOnlyList<string>>(), It.IsAny<List<Argument>>()))
                .Callback<IReadOnlyList<string>, List<Argument>>((_, y) => arguments = y);

            var sut = new ArgumentsCommand();
            sut.Run(parserMock.Object);

            Assert.Equal(4, arguments.Count);
        }
    }
}