using System;

namespace RoseByte.SharpArgs
{
    public class Result<T> : IResult<T>
    {
        public T Route { get; set; }
        public string[] OriginalArgs { get; set; }
        public string[] CurrentArgs { get; set; }
        public bool DefaultRouteUsed { get; set; }
        public bool Success { get; set; }
        public IServiceProvider Provider { get; set; }
    }
}