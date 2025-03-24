using System;
using System.Collections.Generic;
using Pidgin;

namespace SimpleStateMachine.StructuralSearch.Extensions;

internal static class StringParserExtensions
{
    public static Parser<TToken, string> JoinToString<TToken>(this Parser<TToken, IEnumerable<string>> parser, string? separator = null)
    {
        separator ??= string.Empty;
        return parser.Select(x => string.Join(separator, x));
    }

    public static Parser<TToken, SourceMatch> AsMatch<TToken>(this Parser<TToken, string> parser)
        => parser.Then(Parser<TToken>.CurrentOffset, (s, offset) => new SourceMatch(s, offset - s.Length, offset));

    public static Parser<char, T> TrimEnd<T>(this Parser<char, T> parser)
        => parser.Before(Parser.SkipWhitespaces);

    public static Parser<char, T> TrimStart<T>(this Parser<char, T> parser)
        => parser.After(Parser.SkipWhitespaces);

    public static Parser<char, T> Trim<T>(this Parser<char, T> parser)
        => parser.Between(Parser.SkipWhitespaces);

    public static Parser<char, TEnum> AsEnum<TEnum>(this Parser<char, string> parser, bool ignoreCase)
        where TEnum : struct, Enum
        => parser.Select(value => Enum.Parse<TEnum>(value, ignoreCase));
}