namespace RoseByte.SharpArgs.Internal.Parser.Parts
{
    public interface IPart
    {
        string Content { get; }
        string Label { get; }
        string Value { get; }
        bool IsOption { get; }
        bool IsFlag { get; }
    }
}