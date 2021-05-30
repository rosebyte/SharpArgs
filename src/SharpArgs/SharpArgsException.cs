using System;

namespace RoseByte.SharpArgs
{
    public class SharpArgsException : Exception
    {
        public SharpArgsException(string message) : base(message) { }
        public SharpArgsException(string message, Exception exception) : base(message, exception) { }

        public static SharpArgsException UnbindableParameter(string name)
        {
            return new SharpArgsException($"the parameter '{name}' couldn't be bound");
        }
        
        public static SharpArgsException UnbindableParameter(char name)
        {
            return new SharpArgsException($"the parameter '{name}' couldn't be bound");
        }
        
        public static SharpArgsException UnbindableOption(int order)
        {
            return new SharpArgsException($"the option on position '{order}' couldn't be bound");
        }
        
        public static SharpArgsException UnbindableGroup(string name, Exception exception)
        {
            return new SharpArgsException($"the flag group '{name}' couldn't be bound", exception);
        }
    }
}