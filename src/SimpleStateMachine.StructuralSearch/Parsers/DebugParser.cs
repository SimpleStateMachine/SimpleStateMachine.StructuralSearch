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
        
        public override bool TryParse(ref ParseState<TToken> state, ref PooledList<Expected<TToken>> expected, out T result)
        {
            var res = _parser.TryParse(ref state, ref expected, out result);
            return res;
        }
    }
}