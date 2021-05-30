using RoseByte.SharpArgs;

namespace SharpArgs.Tests.TestObjects.Arguments.Options
{
    public class FirstStringOption : Option<string>
    {
        public override string DefaultValue => "FirstDefaultValue";
        public override int? Order => 1;
        public override string Description => "This is the first option";
    }
}