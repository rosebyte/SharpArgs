using RoseByte.SharpArgs;

namespace SharpArgs.Tests.TestObjects.Commands
{
    public class ParserCommand : Command
    {
        public Flag Flag { get; }
        public Option<bool> Option { get; }

        public ParserCommand(Flag flag, Option<bool> option)
        {
            Flag = flag;
            Option = option;
        }
    }
}