using System;
using System.Collections.Generic;
using Pidgin;

namespace SimpleStateMachine.StructuralSearch
{
    public abstract class ParserWithLookahead<TToken, T> : Parser<TToken, T>
    {
        private Func<Parser<TToken, T>> _parser { get; set; }

        public Func<IEnumerable<LookaheadResult<TToken, T>>> OnLookahead { get; set; }

        public abstract Parser<TToken, T> BuildParser(Func<Parser<TToken, T>?> next,
            Func<Parser<TToken, T>?> nextNext);

        public void Lookahead(Func<Parser<TToken, T>?> next, Func<Parser<TToken, T>?> nextNext)
        {
            // lazy initialization
            _parser = () => BuildParser(next, nextNext);
        }

        public override bool TryParse(ref ParseState<TToken> state, ref PooledList<Expected<TToken>> expected,
            out T result)
        {
            var res = _parser().TryParse(ref state, ref expected, out result);
            return res;
        }
    }

    public class LookaheadResult<TToken, T>
    {
        public LookaheadResult(Parser<TToken, T> parser, T result, int tokensCount)
        {
            Parser = parser;
            Result = result;
            TokensCount = tokensCount;
        }

        public T Result { get; }
        public int TokensCount { get; }
        public Parser<TToken, T> Parser;
    }
}