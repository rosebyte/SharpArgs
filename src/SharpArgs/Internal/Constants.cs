using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("RoseByte.SharpArgs.Tests")]
namespace RoseByte.SharpArgs.Internal
{
    internal static class Constants
    {
        internal static class Exceptions
        {
            internal const string ServiceCollectionAlreadyUsed = "SharpArgs alredy initialized in this IServiceCollection.";
        }
    }
}