using System;
using System.Collections.Generic;
using Pidgin;

namespace SimpleStateMachine.StructuralSearch.CustomParsers;

internal abstract class ParserWithLookahead<TToken, T> : Parser<TToken, T>
{
    private Lazy<Parser<TToken, T>>? _lookaheadParser;
    protected Lazy<Parser<TToken, T>> LookaheadParser => _lookaheadParser ?? throw new ArgumentNullException(nameof(Lookahead));
        
    public Func<IEnumerable<LookaheadResult<TToken, T>>>? OnLookahead { get; protected set; }

    protected abstract Parser<TToken, T> BuildParser(Func<Parser<TToken, T>?> next,
        Func<Parser<TToken, T>?> nextNext);

    public void Lookahead(Func<Parser<TToken, T>?> next, Func<Parser<TToken, T>?> nextNext)
    {
        _lookaheadParser = new Lazy<Parser<TToken, T>>(() => BuildParser(next, nextNext));
    }

    public override bool TryParse(ref ParseState<TToken> state, ref PooledList<Expected<TToken>> expected, out T result)
        => LookaheadParser.Value.TryParse(ref state, ref expected, out result!);
}

internal class LookaheadResult<TToken, T>
{
    public T Result { get; }
    public int TokensCount { get; }
    public readonly Parser<TToken, T> Parser;
        
    public LookaheadResult(Parser<TToken, T> parser, T result, int tokensCount)
    {
        Parser = parser;
        Result = result;
        TokensCount = tokensCount;
    }
}