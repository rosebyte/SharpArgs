using System;
using System.Collections.Generic;

namespace RoseByte.SharpArgs.Internal.Helpers
{
    internal class TypeHelper
    {
        internal IReadOnlyList<Type> Types { get; }
        internal TypeHelper(IReadOnlyList<Type> types) => Types = types;
    }
}