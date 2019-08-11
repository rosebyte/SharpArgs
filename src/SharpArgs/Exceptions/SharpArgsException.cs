using System;

namespace RoseByte.SharpArgs.Exceptions
{
    public class SharpArgsException : Exception
    {
        public SharpArgsException(string message) : base(message) { }
    }
}