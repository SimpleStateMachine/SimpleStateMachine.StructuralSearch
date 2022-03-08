using System;
using Pidgin;

namespace SimpleStateMachine.StructuralSearch.Sandbox.Custom
{
    public abstract class LookaheadParser<TToken, T> : Parser<TToken, T>
    {
        private Parser<TToken, T> _parser { get; set; }

        public abstract Parser<TToken, T> BuildParser<Res1, Res2>(Func<Parser<TToken, Res1>> next,
            Func<Parser<TToken, Res2>> nextNext);
        
        public void Lookahead<Res1, Res2>(Func<Parser<TToken, Res1>> next, Func<Parser<TToken, Res2>> nextNext)
        {
            _parser = BuildParser(next, nextNext);
        }
        
        public override bool TryParse(ref ParseState<TToken> state, ref PooledList<Expected<TToken>> expected,
            out T result)
        {
            return _parser.TryParse(ref state, ref expected, out result);
        }
    }
}