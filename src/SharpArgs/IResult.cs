using System;

namespace RoseByte.SharpArgs
{
    public interface IResult<out T>
    {
        T Route { get; }
        string[] OriginalArgs { get; }
        string[] CurrentArgs { get; }
        bool Success { get; }
        IServiceProvider Provider { get; }
    }
}