using RoseByte.SharpArgs.Internal.Routes.BaseClasses;

namespace RoseByte.SharpArgs
{
    public interface IRoute : IRouteBase
    {
        void Execute();
    }
}