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
        
        public Result<char, T> Parse<T>(Parser<char, T> parser)
        {
            return parser.Parse(Input);
        }
    }
}