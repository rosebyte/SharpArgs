using System;
using System.Collections.Generic;

namespace RoseByte.SharpArgs.Internal.Helpers
{
    public interface ITypeHelper<T>
    {
        IReadOnlyList<Type> Types { get; }
    }
}