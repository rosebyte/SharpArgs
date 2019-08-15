using RoseByte.SharpArgs.Internal.Parser;
using RoseByte.SharpArgs.Internal.Parser.Helpers;
using RoseByte.SharpArgs.Internal.Parser.Options;
using RoseByte.SharpArgs.Tests.TestObjects;
using Xunit;

namespace RoseByte.SharpArgs.Tests.Internal.Parser
{
    public class OptionsParserTest
    {
        [Fact]
        public void ShouldParse()
        {
            var obj = new BindingTest();
            var helper = new ParsingHelper(obj);
            var options = new ParsingOptions
            {
                Delimiters = new []{':', ' '}
            };
            var args = new[] {"-f", "-g:true", "--FlagOpt", "--Option", "value 1", "--optionVal:value 2"};
            
            var sut = new OptionsParser();
            sut.ParseParams(args, options, helper);
            
            Assert.True(obj.Flag);
            Assert.True(obj.FlagVal);
            Assert.True(obj.FlagOpt);
            Assert.Equal("value 1", obj.Option);
            Assert.Equal("value 2", obj.OptionVal);
        }
    }
}