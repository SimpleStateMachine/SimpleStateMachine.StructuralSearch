using System.Collections.Generic;
using System.Text;
using Pidgin;

namespace SimpleStateMachine.StructuralSearch.StructuralSearch;

internal static class Grammar
{
    internal static readonly Parser<char, char> NonLanguageSyntaxChar = Parser.AnyCharExcept(Constant.LanguageSyntaxChars);
    internal static readonly Parser<char, char> StringLiteralChar = Parser.AnyCharExcept(Constant.InvalidStringLiteralChars);
    internal static readonly Parser<char, string> WhiteSpaces = Parser.OneOf(CommonParser.Spaces, CommonParser.LineEnds, CommonParser.LineEnds).AtLeastOnceString();

    internal static readonly Parser<char, string> StringLiteral = Parser.OneOf
    (
        Parser.AnyCharExcept(Constant.StringLiteralCharsToEscape),
        CommonParser.Escaped(Constant.StringLiteralCharsToEscape)
    ).AtLeastOnceString().Between(CommonParser.DoubleQuotes);

    internal static readonly Parser<char, string> Identifier = Parser.Letter.Then(Parser.OneOf(Parser.Letter, Parser.Digit, CommonParser.Underscore).AtLeastOnce(), BuildString);
    internal static readonly Parser<char, string> Placeholder = Identifier.Between(Parser.Char(Constant.PlaceholderSeparator));

    private static string BuildString(char c, IEnumerable<char> chars)
    {
        var builder = new StringBuilder();
        builder.Append(c);

        foreach (var value in chars)
            builder.Append(value);

        return builder.ToString();
    }
}