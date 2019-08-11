using System;

namespace RoseByte.SharpArgs.Internal.Exceptions
{
    public class SharpArgsException : Exception
    {
        public SharpArgsException(string message) : base(message) { }
    }
}