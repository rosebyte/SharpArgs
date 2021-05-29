using System;

namespace RoseByte.SharpArgs.Exceptions
{
    public class SharpArgsException : Exception
    {
        
        internal const string ServiceCollectionAlreadyUsed = "SharpArgs alredy initialized in this IServiceCollection.";
        public SharpArgsException(string message) : base(message) { }
    }
}