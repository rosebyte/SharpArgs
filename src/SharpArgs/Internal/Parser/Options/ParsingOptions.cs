using System.Collections.Generic;

namespace RoseByte.SharpArgs.Internal.Parser.Options
{
    internal class ParsingOptions : IParsingOptions
    {
        public string FlagPrefix { get; set; } = "-";
        public string OptionPrefix { get; set; } = "--";
        public bool CanCombineFlags { get; set; } = false;
        public IList<char> Delimiters { get; set; } = new List<char>{' '};
    }
}