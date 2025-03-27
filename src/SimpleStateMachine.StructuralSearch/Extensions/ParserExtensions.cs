using System;
using Pidgin;
using System.Collections.Generic;
using SimpleStateMachine.StructuralSearch.StructuralSearch;

namespace SimpleStateMachine.StructuralSearch.Extensions;

internal static class ParserExtensions
{
    public static Parser<TToken, T> Try<TToken, T>(this Parser<TToken, T> parser)
        => Parser.Try(parser ?? throw new ArgumentNullException(nameof(parser)));

    public static Parser<TToken, IEnumerable<T>> AsMany<TToken, T>(this Parser<TToken, T> parser)
        => parser.Select(IEnumerable<T> (x) => [x]);

    public static Parser<TToken, T> AsLazy<TToken, T>(this Func<Parser<TToken, T>> parser)
        => Parser.Rec(() => parser() ?? throw new ArgumentNullException(nameof(parser)));

    public static Parser<TToken, IEnumerable<T>> AtLeastOnceUntilNot<TToken, T, U>(this Parser<TToken, T> parser,
        Parser<TToken, U> terminator) =>
        parser != null
            ? parser.AtLeastOnceUntil(Parser.Not(terminator))
            : throw new ArgumentNullException(nameof(parser));

    public static bool TryParse(this Parser<char, string> parser, string value, out string? result)
    {
        ArgumentNullException.ThrowIfNull(parser);

        var res = parser.Parse(value);
        result = res.Success ? res.Value : default;
        return res.Success;
    }

    public static Parser<TToken, T> ThenInvoke<TToken, T>(this Parser<TToken, T> parser, Action<T> action)
        => parser.Select(x =>
        {
            action.Invoke(x);
            return x;
        });

    public static Parser<char, Match<T>> Match<T>(this Parser<char, T> parser)
        => Parser.Map((oldPos, oldOffset, result, newPos, newOffset) =>
            {
                var line = new LinePosition(oldPos.Line, newPos.Line);
                var column = new ColumnPosition(oldPos.Col, newPos.Col);
                var offset = new OffsetPosition(oldOffset, newOffset);
                var lenght = newOffset - oldOffset;
                return new Match<T>(result, lenght, column, line, offset);
            },
            Parser<char>.CurrentPos, Parser<char>.CurrentOffset,
            parser,
            Parser<char>.CurrentPos, Parser<char>.CurrentOffset);

    public static Parser<TToken, T> WithDebug<TToken, T>(this Parser<TToken, T> parser, string label)
        => Parser.Map((u, t, _) =>
        {
            Console.WriteLine($"{label} ({t.Col}) : {u} ");
            return u;
        }, parser, Parser<TToken>.CurrentPos, Parser<TToken>.CurrentSourcePosDelta);

    public static Parser<TToken, T> WithDebug<TToken, T>(this Parser<TToken, T> parser) =>
        Parser.Map((u, t, _) =>
        {
            Console.WriteLine($"({t.Col}) : {u} ");
            return u;
        }, parser, Parser<TToken>.CurrentPos, Parser<TToken>.CurrentSourcePosDelta);

    public static Parser<TToken, TInterface> As<TToken, TClass, TInterface>(this Parser<TToken, TClass> parser)
        where TClass : TInterface => parser.Select(x => (TInterface)x);

    public static Parser<TToken, T> After<TToken, T, TNextToken>(this Parser<TToken, T> parser,
        Parser<TToken, TNextToken> parserAfter)
        => parserAfter.Then(parser, (_, t) => t);

    // TODO optimization
    public static Parser<char, T> ParenthesisedOptional<T, TResult>(this Parser<char, T> parser,
        Func<char, Parser<char, TResult>> custom)
        => Parser.OneOf(CommonParser.Parenthesised(parser, custom).Try(), parser);

    public static Parser<TToken, Parser<TToken, T>> SelectToParser<TToken, T>(this Parser<TToken, T> parser,
        Func<T, Parser<TToken, T>, Parser<TToken, T>> selectFunc)
        => parser.Select(value => selectFunc(value, parser));
}