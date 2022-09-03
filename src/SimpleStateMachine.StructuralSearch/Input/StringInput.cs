using Pidgin;

namespace SimpleStateMachine.StructuralSearch
{
    public class StringInput : IInput
    {
        private readonly string _input;
        
        public StringInput(string input)
        {
            _input = input;
        }

        public Result<char, T> ParseBy<T>(Parser<char, T> parser)
        {
            return parser.Parse(_input);
        }

        public void Replace(Match<string> match, string value)
        {
            throw new System.NotImplementedException();
        }

        public string Extension => string.Empty;
        public string Path => string.Empty;
        public string Name => string.Empty;
        public string Data => string.Empty;
        public long Lenght => _input.Length;
    }
}