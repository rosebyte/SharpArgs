namespace RoseByte.SharpArgs.Internal.Parser
{
    internal class CliParserFactory : ICliParserFactory
    {
        public ICliParser Create() => new CliParser();
    }
}