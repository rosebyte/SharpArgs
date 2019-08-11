using RoseByte.SharpArgs.Internal.Parser;

namespace RoseByte.SharpArgs.Internal.Routers.BaseClasses
{
    public interface IRouter<TRoute>
    {
        IReadOnlyParsingOptions Options { get; }
        bool TryGetRoute<T>(string name, out T route, out bool defaultUsed);
        IRouter<TRoute> WithOptionPrefix(string prefix);
        IRouter<TRoute> WithFlagPrefix(string prefix);
        IRouter<TRoute> WithCombinedFlagsAllowed();
        IRouter<TRoute> WithOptionsDelimitedBy(bool includeSpace, params char[] delimiters);
        IRouter<TRoute> WithDefault<T>() where T: TRoute;
        IRouter<TRoute> WithRoute<T>(string name = null) where  T: TRoute;
    }
}