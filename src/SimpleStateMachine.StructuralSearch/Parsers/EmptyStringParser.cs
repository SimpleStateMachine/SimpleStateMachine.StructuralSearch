using Pidgin;

namespace SimpleStateMachine.StructuralSearch
{
    public class EmptyStringParser : Parser<char, string>
    {
        private readonly bool _value;

        public EmptyStringParser(bool value)
        {
            _value = value;
        }

        public override bool TryParse(ref ParseState<char> state, ref PooledList<Expected<char>> expecteds,
            out string result)
        {
            result = string.Empty;
            return _value;
        }
    }
}