using Pidgin;

namespace SimpleStateMachine.StructuralSearch
{
    public class DebugParser<TToken, T> : Parser<TToken, T>
    {
        private readonly Parser<TToken, T> _parser;
        public DebugParser(Parser<TToken, T> parser)
        {
            _parser = parser;
        }
        
        public override bool TryParse(ref ParseState<TToken> state, ref PooledList<Expected<TToken>> expecteds, out T result)
        {
            var res = _parser.TryParse(ref state, ref expecteds, out result);
            return res;
        }
    }
}