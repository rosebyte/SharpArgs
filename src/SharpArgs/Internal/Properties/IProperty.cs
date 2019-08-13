using System;

namespace RoseByte.SharpArgs.Internal.Properties
{
    public interface IProperty
    {
        int? Order { get; }
        char? Shortcut { get; }
        string Label { get; }
        string Description { get; }
        string Name { get; }
        Type Type { get; }
        void Set(bool value);
        void Set(string value);
        bool Equals(Property other);
        bool Equals(object obj);
        int GetHashCode();
    }
}