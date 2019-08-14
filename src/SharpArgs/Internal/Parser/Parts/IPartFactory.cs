using RoseByte.SharpArgs.Internal.Parser.Options;

namespace RoseByte.SharpArgs.Internal.Parser.Parts
{
    public interface IPartFactory
    {
        IPart Create(string content, IReadOnlyParsingOptions options);
    }
}