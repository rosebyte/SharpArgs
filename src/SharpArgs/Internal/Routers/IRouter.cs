namespace RoseByte.SharpArgs.Internal.Routers
{
    public interface IRouter<TRoute>
    {
        IResult<TRoute> Resolve(string[] args);
    }
}