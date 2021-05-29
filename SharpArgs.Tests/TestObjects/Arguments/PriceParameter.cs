using System;
using RoseByte.SharpArgs;

namespace SharpArgs.Tests.TestObjects.Arguments
{
    public class PriceParameter : Parameter<Decimal>
    {
        public override string Description => "base price";
    }
}