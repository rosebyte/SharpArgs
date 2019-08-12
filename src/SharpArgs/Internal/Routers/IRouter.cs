namespace RoseByte.SharpArgs.Internal.Routers.BaseClasses
{
    public interface IRouter<TRoute>
    {
        IResult<TRoute> Resolve(string[] args);
    }
}