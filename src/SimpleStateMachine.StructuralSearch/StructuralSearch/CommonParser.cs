using System;
using System.Collections.Generic;
using System.Text;
using Pidgin;

namespace SimpleStateMachine.StructuralSearch.StructuralSearch;

internal static class CommonParser
{
    internal static readonly Parser<char, string> Empty = Parsers.Parsers.String(Constant.EmptyString, false);
    internal static readonly Parser<char, char> Space = Parser.Char(Constant.Space);
    internal static readonly Parser<char, char> Underscore = Parser.Char(Constant.Underscore);
    internal static readonly Parser<char, Unit> Eof = Parser<char>.End;
    internal static readonly Parser<char, string> AnyString = Parser.AnyCharExcept(Constant.All).AtLeastOnceString();
    internal static readonly Parser<char, string> Spaces = Space.AtLeastOnceString();
    internal static readonly Parser<char, string> LineEnds = Parser.EndOfLine.AtLeastOnceString();
    internal static readonly Parser<char, string> WhiteSpaces = Parser.OneOf(Spaces, LineEnds, LineEnds).AtLeastOnceString();
    internal static readonly Parser<char, string> Identifier = Parser.Letter.Then(Parser.OneOf(Parser.Letter, Parser.Digit, Underscore).AtLeastOnce(), BuildString);
    internal static readonly Parser<char, char> Comma = Parser.Char(Constant.Comma);
    internal static readonly Parser<char, char> Colon = Parser.Char(Constant.Colon);
    internal static readonly Parser<char, char> DoubleQuotes = Parser.Char(Constant.DoubleQuotes);
    internal static readonly Parser<char, char> SingleQuotes = Parser.Char(Constant.SingleQuotes);
    internal static readonly Parser<char, char> Dote = Parser.Char(Constant.Dote);

    internal static readonly Parser<char, char> PlaceholderSeparator = Parser.Char(Constant.PlaceholderSeparator);

    internal static Parser<char, T> Parenthesised<T, TResult>(Parser<char, T> parser,
        Func<char, Parser<char, TResult>> custom)
        => parser.Between
        (
            custom(Constant.LeftParenthesis),
            custom(Constant.RightParenthesis)
        );

    internal static Parser<char, char> Escaped(IEnumerable<char> chars) =>
        Parser.Char(Constant.BackSlash).Then(Parser.OneOf(chars));

    private static string BuildString(char c, IEnumerable<char> chars)
    {
        var builder = new StringBuilder();
        builder.Append(c);

        foreach (var value in chars)
            builder.Append(value);

        return builder.ToString();
    }
}