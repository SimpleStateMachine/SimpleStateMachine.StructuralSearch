using System;
using Pidgin;
using System.Collections.Generic;

namespace SimpleStateMachine.StructuralSearch.Extensions;

internal static class ParserExtensions
{
    public static Parser<TToken, T> Try<TToken, T>(this Parser<TToken, T> parser)
        => Parser.Try(parser);
    
    public static Parser<TToken, T> Lookahead<TToken, T>(this Parser<TToken, T> parser)
        => Parser.Lookahead(parser);

    public static Parser<TToken, IEnumerable<T>> AtLeastOnceUntilNot<TToken, T, TTerminator>(this Parser<TToken, T> parser,
        Parser<TToken, TTerminator> terminator) =>
        parser != null
            ? parser.AtLeastOnceUntil(Parser.Not(terminator))
            : throw new ArgumentNullException(nameof(parser));

    public static bool TryParse(this Parser<char, string> parser, string value, out string? result)
    {
        ArgumentNullException.ThrowIfNull(parser);

        var res = parser.Parse(value);
        result = res.Success ? res.Value : null;
        return res.Success;
    }

    public static Parser<TToken, T> ThenInvoke<TToken, T>(this Parser<TToken, T> parser, Action<T> action)
        => parser.Select(x =>
        {
            action.Invoke(x);
            return x;
        });

    public static Parser<TToken, T> WithDebug<TToken, T>(this Parser<TToken, T> parser, string label)
        => Parser.Map((u, t, _) =>
        {
            Console.WriteLine($"{label}: [{t.Line},{t.Col}] : {u} ");
            return u;
        }, parser, Parser<TToken>.CurrentPos, Parser<TToken>.CurrentSourcePosDelta);

    public static Parser<TToken, T> WithDebug<TToken, T>(this Parser<TToken, T> parser) =>
        Parser.Map((u, t, _) =>
        {
            Console.WriteLine($"[{t.Line},{t.Col}] : {u} ");
            return u;
        }, parser, Parser<TToken>.CurrentPos, Parser<TToken>.CurrentSourcePosDelta);

    public static Parser<TToken, T> After<TToken, T, TNextToken>(this Parser<TToken, T> parser,
        Parser<TToken, TNextToken> parserAfter)
        => parserAfter.Then(parser, (_, t) => t);

    public static Parser<TToken, Parser<TToken, T>> SelectToParser<TToken, T>(this Parser<TToken, T> parser,
        Func<T, Parser<TToken, T>, Parser<TToken, T>> selectFunc)
        => parser.Select(value => selectFunc(value, parser));
}