using System;
using System.Collections.Generic;
using System.Linq;
using Pidgin;
using SimpleStateMachine.StructuralSearch.Extensions;

namespace SimpleStateMachine.StructuralSearch.Parsers;

internal static class Parsers
{
    // private static Parser<TToken, IEnumerable<T>> MapToMany<TToken, T>(Parser<TToken, T> parser1,
    //     Parser<TToken, T> parser2, Parser<TToken, T> parser3)
    // {
    //     ArgumentNullException.ThrowIfNull(parser1);
    //     ArgumentNullException.ThrowIfNull(parser2);
    //     ArgumentNullException.ThrowIfNull(parser3);
    //
    //     return Parser.Map((arg1, arg2, arg3) => (IEnumerable<T>)new List<T> { arg1, arg2, arg3 }, parser1, parser2, parser3);
    // }
    //
    // private static Parser<TToken, IEnumerable<T>> MapToMany<TToken, T>(Parser<TToken, T> parser1,
    //     Parser<TToken, IEnumerable<T>> parser2, Parser<TToken, T> parser3)
    // {
    //     ArgumentNullException.ThrowIfNull(parser1);
    //     ArgumentNullException.ThrowIfNull(parser2);
    //     ArgumentNullException.ThrowIfNull(parser3);
    //
    //     return Parser.Map((arg1, arg2, arg3) =>
    //     {
    //         var result = arg2.ToList();
    //         result.Insert(0, arg1);
    //         result.Add(arg3);
    //         return (IEnumerable<T>)result;
    //     }, parser1, parser2, parser3);
    // }

    public static Parser<char, TResult> BetweenParentheses<T, TResult>(Parser<char, T> expr, Func<char, T, char, TResult> mapFunc)
    {
        var parsers = Constant.AllParentheses.Select(pair => Parser.Map(mapFunc, Parser.Char(pair.Item1), expr, Parser.Char(pair.Item1)));
        return Parser.OneOf(parsers);
    }

    // public static Parser<char, IEnumerable<T>> BetweenOneOfChars<T>(Func<char, Parser<char, T>> leftRight, Parser<char, IEnumerable<T>> expr, params (char, char)[] values) 
    //     => Parser.OneOf(values.Select(x => MapToMany(leftRight(x.Item1), expr, leftRight(x.Item2))));
    //
    // public static Parser<char, IEnumerable<T>> BetweenOneOfChars<T>(Func<char, Parser<char, T>> leftRight, Parser<char, T> expr, params (char, char)[] values) 
    //     => Parser.OneOf(values.Select(x => MapToMany(leftRight(x.Item1), expr, leftRight(x.Item2))));
    //
    // public static Parser<char, (TResult, T)> BetweenOneOfChars<T, TResult>(Func<char, char, TResult> resultFunc, Parser<char, T> expr, params (char, char)[] values) 
    //     => Parser.OneOf(values.Select(x => Parser.Map((c1, res, c2) => (resultFunc(c1, c2), res), Parser.Char(x.Item1).Try(), expr, Parser.Char(x.Item2).Try())));

    public static Parser<char, TEnum> EnumValue<TEnum>(TEnum value)
        where TEnum : struct, Enum 
        => Parser.CIString(value.ToString()).AsEnum<TEnum>(true);
}