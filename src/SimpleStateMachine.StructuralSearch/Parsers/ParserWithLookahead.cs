using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Pidgin;
using SimpleStateMachine.StructuralSearch.Extensions;

namespace SimpleStateMachine.StructuralSearch
{
    public abstract class ParserWithLookahead<TToken, T> : Parser<TToken, T>
    {
        protected Lazy<Parser<TToken, T>> parser;

        public Func<IEnumerable<LookaheadResult<TToken, T>>> OnLookahead { get; set; }

        public abstract Parser<TToken, T> BuildParser(Func<Parser<TToken, T>?> next,
            Func<Parser<TToken, T>?> nextNext);

        public void Lookahead(Func<Parser<TToken, T>?> next, Func<Parser<TToken, T>?> nextNext)
        {
            parser = new Lazy<Parser<TToken, T>>(() => BuildParser(next, nextNext));
        }

        public override bool TryParse(ref ParseState<TToken> state, ref PooledList<Expected<TToken>> expected,
            out T result)
        {
            var res = parser.Value.TryParse(ref state, ref expected, out result);
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