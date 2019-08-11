using System;
using System.Collections.Generic;

namespace RoseByte.SharpArgs.Internal.Helpers
{
    public class TypeHelper
    {
        public IReadOnlyList<Type> Types { get; }
        public TypeHelper(IReadOnlyList<Type> types) => Types = types;
    }
}