using System.Threading.Tasks;
using RoseByte.SharpArgs.Internal.Routes.BaseClasses;

namespace RoseByte.SharpArgs
{
    public interface IAsyncRoute : IRouteBase
    {
        Task Execute();
    }
}