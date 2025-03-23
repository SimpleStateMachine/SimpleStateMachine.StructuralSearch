using Pidgin;

namespace SimpleStateMachine.StructuralSearch.Parsers;

internal class EmptyStringParser : Parser<char, string>
{
    private readonly bool _value;

    public EmptyStringParser(bool value)
    {
        _value = value;
    }

    public override bool TryParse(ref ParseState<char> state, ref PooledList<Expected<char>> expected, out string result)
    {
        result = string.Empty;
        return _value;
    }
}