using RoseByte.SharpArgs.Internal.Parser.Options;

namespace RoseByte.SharpArgs.Internal.Parser.Parts
{
    public class PartFactory : IPartFactory
    {
        public IPart Create(string content, IParsingOptions options)
        {
            return new Part(content, options);
        }
    }
}