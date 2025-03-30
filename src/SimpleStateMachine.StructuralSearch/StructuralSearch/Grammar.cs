using System;
using System.Collections.Generic;
using System.Text;
using Pidgin;
using SimpleStateMachine.StructuralSearch.Parameters.Types;

namespace SimpleStateMachine.StructuralSearch.StructuralSearch;

internal static class Grammar
{
    internal static readonly Parser<char, string> WhiteSpaces = Parser.OneOf(Constant.WhitespaceChars).AtLeastOnceString();
    
    internal static readonly Parser<char, string> TemplateStringLiteral =
        Parser.AnyCharExcept(Constant.InvalidStringLiteralChars).AtLeastOnceString();

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
    
    internal static ParenthesisType GetParenthesisType((char c1, char c2) parenthesis)
        => parenthesis switch
    {
        (Constant.LeftParenthesis, Constant.RightParenthesis) => ParenthesisType.Usual,
        (Constant.LeftSquareParenthesis, Constant.RightSquareParenthesis) => ParenthesisType.Square,
        (Constant.LeftCurlyParenthesis, Constant.RightCurlyParenthesis) => ParenthesisType.Curly,
        _ => throw new ArgumentOutOfRangeException(nameof(parenthesis), parenthesis, null)
    };
}