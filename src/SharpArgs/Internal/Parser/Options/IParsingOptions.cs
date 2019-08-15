using System.Collections.Generic;

namespace RoseByte.SharpArgs.Internal.Parser.Options
{
    public interface IParsingOptions
    {
        string FlagPrefix { get; }
        string OptionPrefix { get; }
        bool CanCombineFlags { get; }
        IList<char> Delimiters { get; }
    }
}