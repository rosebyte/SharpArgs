using RoseByte.SharpArgs;

namespace SharpArgs.Tests.TestObjects.Arguments.Options
{
    public class ThirdBoolOption : Option<bool>
    {
        public override bool DefaultValue => true;
        public override int? Order => 3;
        public override string Description => "This is the third option";
    }
}