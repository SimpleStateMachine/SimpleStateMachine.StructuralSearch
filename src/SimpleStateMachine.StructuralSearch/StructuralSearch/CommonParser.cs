using System;
using System.Collections.Generic;
using Pidgin;

namespace SimpleStateMachine.StructuralSearch.StructuralSearch;

internal static class CommonParser
{
    internal static readonly Parser<char, char> Underscore = Parser.Char(Constant.Underscore);
    internal static readonly Parser<char, Unit> Eof = Parser<char>.End;
    internal static readonly Parser<char, char> Comma = Parser.Char(Constant.Comma);
    internal static readonly Parser<char, char> DoubleQuotes = Parser.Char(Constant.DoubleQuotes);
    internal static readonly Parser<char, char> Dote = Parser.Char(Constant.Dote);
    internal static readonly Parser<char, char> EscapedChar = Escaped(Constant.CharsToEscape);
    internal static readonly Parser<char, string> Spaces = Parser.Char(Constant.Space).AtLeastOnceString();
    internal static readonly Parser<char, string> LineEnds = Parser.EndOfLine.AtLeastOnceString();
    internal static readonly Parser<char, string> Should = Parser.CIString(Constant.Should);

    internal static readonly Parser<char, string> If = Parser.String(Constant.If);
    internal static readonly Parser<char, string> Then = Parser.CIString(Constant.Then);
    internal static readonly Parser<char, string> Not = Parser.CIString(Constant.Not);
    internal static readonly Parser<char, string> Is = Parser.CIString(Constant.Is);
    internal static readonly Parser<char, string> Match = Parser.CIString(Constant.Match);
    internal static readonly Parser<char, string> In = Parser.CIString(Constant.In);
    internal static readonly Parser<char, string> Length = Parser.CIString(Constant.Length);
    internal static readonly Parser<char, string> Input = Parser.CIString(Constant.Input);

    internal static readonly Parser<char, char> LeftParenthesis = Parser.Char(Constant.LeftParenthesis);
    internal static readonly Parser<char, char> RightParenthesis = Parser.Char(Constant.RightParenthesis);
    internal static readonly Parser<char, char> LeftSquareParenthesis = Parser.Char(Constant.LeftSquareParenthesis);
    internal static readonly Parser<char, char> RightSquareParenthesis = Parser.Char(Constant.RightSquareParenthesis);
    internal static readonly Parser<char, char> LeftCurlyParenthesis = Parser.Char(Constant.LeftCurlyParenthesis);
    internal static readonly Parser<char, char> RightCurlyParenthesis = Parser.Char(Constant.RightCurlyParenthesis);

    internal static Parser<char, T> Parenthesised<T, TResult>(Parser<char, T> parser,
        Func<char, Parser<char, TResult>> custom)
        => parser.Between
        (
            custom(Constant.LeftParenthesis),
            custom(Constant.RightParenthesis)
        );

    internal static Parser<char, char> Escaped(IEnumerable<char> chars) =>
        Parser.Char(Constant.BackSlash).Then(Parser.OneOf(chars));
}