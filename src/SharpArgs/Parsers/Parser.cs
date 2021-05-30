namespace RoseByte.SharpArgs.Parsers
{
    public static class Parser
    {
        public static IArgumentsParser Unix => new UnixArgumentsParser();
    }
}