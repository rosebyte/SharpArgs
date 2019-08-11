using System;
using System.Collections.Generic;

namespace RoseByte.SharpArgs.Internal.Helpers
{
    internal class TypeHelper<T> : ITypeHelper<T>
    {
        public IReadOnlyList<Type> Types { get; }
        public TypeHelper(IReadOnlyList<Type> types) => Types = types;
    }
}