using Pidgin;

namespace SimpleStateMachine.StructuralSearch
{
    public class StringInput : IInput
    {
        public StringInput(string input)
        {
            Input = input;
        }
        
        public readonly string Input;
        
        public Result<char, T> ParseBy<T>(Parser<char, T> parser)
        {
            return parser.Parse(Input);
        }

        public void ReplaceAsync(Match<string> match, string value)
        {
            throw new System.NotImplementedException();
        }

        public string Extension => string.Empty;
        public string Path => string.Empty;
        public string Name => string.Empty;
        public string Data => string.Empty;
        public long Lenght => Input.Length;
    }
}