using System;
using System.Collections.Generic;
using System.Linq;
using Pidgin;
using SimpleStateMachine.StructuralSearch.Extensions;
using static Pidgin.Parser;

namespace SimpleStateMachine.StructuralSearch.Parsers;

internal static class Parsers
{
    public static Parser<char, string> String(string value, bool ignoreCase) 
        => ignoreCase ? CIString(value): Parser.String(value);

    public static Parser<char, TEnum> After<TEnum>(TEnum value, bool ignoreCase = false)
        where TEnum : struct, Enum 
        => String(value.ToString(), ignoreCase).AsEnum<TEnum>(ignoreCase);

    private static Parser<TToken, IEnumerable<T>> MapToMany<TToken, T>(Parser<TToken, T> parser1,
        Parser<TToken, T> parser2, Parser<TToken, T> parser3)
    {
        ArgumentNullException.ThrowIfNull(parser1);
        ArgumentNullException.ThrowIfNull(parser2);
        ArgumentNullException.ThrowIfNull(parser3);

        return Map((arg1, arg2, arg3) => (IEnumerable<T>)new List<T> { arg1, arg2, arg3 }, parser1, parser2, parser3);
    }

    private static Parser<TToken, IEnumerable<T>> MapToMany<TToken, T>(Parser<TToken, T> parser1,
        Parser<TToken, IEnumerable<T>> parser2, Parser<TToken, T> parser3)
    {
        ArgumentNullException.ThrowIfNull(parser1);
        ArgumentNullException.ThrowIfNull(parser2);
        ArgumentNullException.ThrowIfNull(parser3);

        return Map((arg1, arg2, arg3) =>
        {
            var result = arg2.ToList();
            result.Insert(0, arg1);
            result.Add(arg3);
            return (IEnumerable<T>)result;
        }, parser1, parser2, parser3);
    }

    public static Parser<TToken, IEnumerable<T>> MapToMany<TToken, T>(Parser<TToken, T> parser1,
        Parser<TToken, T> parser2)
    {
        if (parser1 == null)
            throw new ArgumentNullException(nameof(parser1));
        if (parser2 == null)
            throw new ArgumentNullException(nameof(parser2));

        return Map((arg1, arg2) =>
        {
            var result = new List<T> { arg1, arg2 };
            return (IEnumerable<T>)result;
        }, parser1, parser2);
    }

    public static Parser<char, IEnumerable<T>> BetweenChars<T>(char left, char right,
        Func<char, Parser<char, T>> leftRight,
        Parser<char, IEnumerable<T>> expr)
        => MapToMany(leftRight(left), expr, leftRight(right));


    public static Parser<char, IEnumerable<T>> BetweenOneOfChars<T>(Func<char, Parser<char, T>> leftRight, Parser<char, IEnumerable<T>> expr, params (char, char)[] values) 
        => OneOf(values.Select(x => MapToMany(leftRight(x.Item1), expr, leftRight(x.Item2))));

    public static Parser<char, IEnumerable<T>> BetweenOneOfChars<T>(Func<char, Parser<char, T>> leftRight, Parser<char, T> expr, params (char, char)[] values) 
        => OneOf(values.Select(x => MapToMany(leftRight(x.Item1), expr, leftRight(x.Item2))));

    public static Parser<char, (TResult, T)> BetweenOneOfChars<T, TResult>(Func<char, char, TResult> resultFunc, Parser<char, T> expr, params (char, char)[] values) 
        => OneOf(values.Select(x => Map((c1, res, c2) => (resultFunc(c1, c2), res), Char(x.Item1).Try(), expr, Char(x.Item2).Try())));

    public static Parser<char, TEnum> EnumExcept<TEnum>(bool ignoreCase = false, params TEnum[] excluded)
        where TEnum : struct, Enum 
        => new EnumParser<TEnum>(ignoreCase, excluded);

    public static Parser<char, TEnum> EnumValue<TEnum>(TEnum value, bool ignoreCase = false)
        where TEnum : struct, Enum 
        => String(value.ToString(), ignoreCase).AsEnum<TEnum>(ignoreCase);

    public static Parser<char, Match<T>> Match<T>(Parser<char, T> parser) 
        => Map((oldPos, oldOffset, result, newPos, newOffset) =>
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
}