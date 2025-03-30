using System;
using System.Collections.Generic;
using Pidgin;
using SimpleStateMachine.StructuralSearch.Parsing;

namespace SimpleStateMachine.StructuralSearch.Extensions;

internal static class StringParserExtensions
{
    public static Parser<char, TResult> BetweenAnyParentheses<T, TResult>(this Parser<char, T> parser, Func<char, T, char, TResult> mapFunc)
    {
        var parentheses= parser.BetweenParentheses(mapFunc);
        var curlyParentheses= parser.BetweenCurlyParentheses(mapFunc);
        var squareParentheses= parser.BetweenSquareParentheses(mapFunc);
        return Parser.OneOf(parentheses, curlyParentheses, squareParentheses);
    }

    public static Parser<char, TResult> BetweenParentheses<T, TResult>(this Parser<char, T> parser, Func<char, T, char, TResult> mapFunc)
        => Parser.Map(mapFunc, CommonParser.LeftParenthesis, parser, CommonParser.RightParenthesis);

    public static Parser<char, TResult> BetweenCurlyParentheses<T, TResult>(this Parser<char, T> parser, Func<char, T, char, TResult> mapFunc)
        => Parser.Map(mapFunc, CommonParser.LeftCurlyParenthesis, parser, CommonParser.RightCurlyParenthesis);

    public static Parser<char, TResult> BetweenSquareParentheses<T, TResult>(this Parser<char, T> parser, Func<char, T, char, TResult> mapFunc)
        => Parser.Map(mapFunc, CommonParser.LeftSquareParenthesis, parser, CommonParser.RightSquareParenthesis);

    public static T ParseToEnd<T>(this Parser<char, T> parser, string str)
        => parser.Before(CommonParser.Eof).ParseOrThrow(str);

    public static Parser<TToken, string> JoinToString<TToken>(this Parser<TToken, IEnumerable<string>> parser, string? separator = null)
    {
        separator ??= string.Empty;
        return parser.Select(x => string.Join(separator, x));
    }

    public static Parser<char, T> TrimEnd<T>(this Parser<char, T> parser)
        => parser.Before(Parser.SkipWhitespaces);

    public static Parser<char, T> TrimStart<T>(this Parser<char, T> parser)
        => parser.After(Parser.SkipWhitespaces);

    public static Parser<char, T> Trim<T>(this Parser<char, T> parser)
        => parser.Between(Parser.SkipWhitespaces);

    public static Parser<char, Match<T>> Match<T>(this Parser<char, T> parser)
        => Parser.Map((oldPos, oldOffset, result, newPos, newOffset) =>
            {
                var line = new LinePosition(oldPos.Line, newPos.Line);
                var column = new ColumnPosition(oldPos.Col, newPos.Col);
                var offset = new OffsetPosition(oldOffset, newOffset);
                var length = newOffset - oldOffset;
                return new Match<T>(result, length, column, line, offset);
            },
            Parser<char>.CurrentPos, Parser<char>.CurrentOffset,
            parser,
            Parser<char>.CurrentPos, Parser<char>.CurrentOffset);
}