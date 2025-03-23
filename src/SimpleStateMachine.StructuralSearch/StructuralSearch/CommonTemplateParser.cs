using System.Linq;
using Pidgin;

namespace SimpleStateMachine.StructuralSearch.StructuralSearch;

internal static class CommonTemplateParser
{
    internal static readonly Parser<char, char> StringLiteralChar = Parser.AnyCharExcept(Constant.InvalidStringLiteralChars);
    internal static readonly Parser<char, string> Placeholder = CommonParser.Identifier.Between(CommonParser.PlaceholderSeparator);
    internal static readonly Parser<char, string> StringLiteral = StringLiteralChar.AtLeastOnceString();
    internal static readonly Parser<char, string> Should = Parser.String(Constant.Should);
}