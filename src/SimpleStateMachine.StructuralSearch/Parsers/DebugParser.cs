using Pidgin;
#pragma warning disable CS9074 // The 'scoped' modifier of parameter doesn't match overridden or implemented member.

namespace SimpleStateMachine.StructuralSearch;

internal class DebugParser<TToken, T> : Parser<TToken, T>
{
    private readonly Parser<TToken, T> _parser;
    public DebugParser(Parser<TToken, T> parser)
    {
        _parser = parser;
    }
        
    public override bool TryParse(ref ParseState<TToken> state, ref PooledList<Expected<TToken>> expected, out T result)
        => _parser.TryParse(ref state, ref expected, out result!);
}