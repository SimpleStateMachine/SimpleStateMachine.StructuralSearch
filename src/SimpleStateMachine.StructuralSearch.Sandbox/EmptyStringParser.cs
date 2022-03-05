using Pidgin;

namespace SimpleStateMachine.StructuralSearch.Sandbox
{
    public class EmptyStringParser:Parser<char, string>
    {
        public bool Value { get; }
        
        public EmptyStringParser(bool value)
        {
            Value = value;
        }
        
        public override bool TryParse(ref ParseState<char> state, ref PooledList<Expected<char>> expecteds, out string result)
        {
            result = string.Empty;
            return Value;
        }
    }
}