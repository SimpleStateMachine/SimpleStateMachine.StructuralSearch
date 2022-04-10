using Pidgin;

namespace SimpleStateMachine.StructuralSearch.Parsers
{
    internal sealed class LookaheadParser<TToken, T> : Parser<TToken, T>
    {
        private readonly Parser<TToken, T> _parser;

        public LookaheadParser(Parser<TToken, T> parser) => _parser = parser;

        public override bool TryParse(ref ParseState<TToken> state, ref PooledList<Expected<TToken>> expecteds, out T result)
        {
            state.PushBookmark();
            if (_parser.TryParse(ref state, ref expecteds, out result))
            {
                state.Rewind();
                return true;
            }
            state.PopBookmark();
            return false;
        }
    }
}