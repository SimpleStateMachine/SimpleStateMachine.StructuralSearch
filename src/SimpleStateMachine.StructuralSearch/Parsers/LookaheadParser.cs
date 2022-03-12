using System;
using Pidgin;

namespace SimpleStateMachine.StructuralSearch
{
    public abstract class LookaheadParser<TToken, T> : Parser<TToken, T>
    {
        private Parser<TToken, T> _parser { get; set; }

        public abstract Parser<TToken, T> BuildParser(Func<Parser<TToken, T>?> next,
            Func<Parser<TToken, T>?> nextNext);
        
        public void Lookahead(Func<Parser<TToken, T>?> next, Func<Parser<TToken, T>?> nextNext)
        {
            _parser = BuildParser(next, nextNext);
        }
        
        public override bool TryParse(ref ParseState<TToken> state, ref PooledList<Expected<TToken>> expected,
            out T result)
        {
            var res = _parser.TryParse(ref state, ref expected, out result);
            return res;
        }
    }
}