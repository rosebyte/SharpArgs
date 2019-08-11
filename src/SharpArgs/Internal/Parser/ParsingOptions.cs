using System.Collections.Generic;

namespace RoseByte.SharpArgs.Internal.Parser
{
    internal class ParsingOptions : IReadOnlyParsingOptions
    {
        public string FlagPrefix { get; set; } = "-";
        public string OptionPrefix { get; set; } = "--";
        public bool CanCombineFlags { get; set; } = false;
        public IList<char> Delimiters { get; set; } = new List<char>{' '};

        public ParsingOptions Clone() => (ParsingOptions)MemberwiseClone();
    }
}