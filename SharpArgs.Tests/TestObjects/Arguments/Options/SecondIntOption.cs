using RoseByte.SharpArgs;

namespace SharpArgs.Tests.TestObjects.Arguments.Options
{
    public class SecondIntOption : Option<int>
    {
        public override int DefaultValue => 5;
        public override int? Order => 2;
        public override string Description => "This is the second option";
    }
}