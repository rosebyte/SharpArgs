using System.Linq;

namespace RoseByte.SharpArgs.Internal.Parser
{
    internal class Part
    {
        public string Content { get; }
        private readonly IReadOnlyParsingOptions _options;

        public string Label
        {
            get
            {
                var result = IsOption 
                    ? Content.Substring(_options.OptionPrefix.Length)
                    : IsFlag 
                        ? Content.Substring(_options.FlagPrefix.Length) 
                        : Content;

                var delimiter = _options.Delimiters.FirstOrDefault(x => result.Contains(x));
                return delimiter == default(char) ? result : result.Split(delimiter).First();
            }
        }

        public string Value
        {
            get
            {
                if (!Content.StartsWith(_options.OptionPrefix) && !Content.StartsWith(_options.FlagPrefix))
                {
                    return Content;
                }
                
                var result = IsOption 
                    ? Content.Substring(_options.OptionPrefix.Length)
                    : IsFlag 
                        ? Content.Substring(_options.FlagPrefix.Length) : Content;

                var delimiter = _options.Delimiters.FirstOrDefault(x => result.Contains(x));
                return delimiter == default(char) 
                    ? null 
                    : string.Join(delimiter, result.Split(delimiter).Skip(1));
            }
        }
        
        public bool IsOption => Content.StartsWith(_options.OptionPrefix);
        
        public bool IsFlag
        {
            get
            {
                if (_options.OptionPrefix.Length > 1 && Content.StartsWith(_options.OptionPrefix))
                {
                    return false;
                }

                return Content.StartsWith(_options.FlagPrefix);
            }
        }
        
        public Part(string content, IReadOnlyParsingOptions options)
        {
            Content = content;
            _options = options;
        }
    }
}