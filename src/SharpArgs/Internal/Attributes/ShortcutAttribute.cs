using System;

namespace RoseByte.SharpArgs.Internal.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ShortcutAttribute : Attribute
    {
        public char Shortcut { get; }

        public ShortcutAttribute(char shortcut) => Shortcut = shortcut;
    }
}