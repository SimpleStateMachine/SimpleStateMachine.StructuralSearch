using System.Linq;
using Pidgin;
using static Pidgin.Parser;

namespace SimpleStateMachine.StructuralSearch.StructuralSearch;

internal static class CommonTemplateParser
{
    internal static readonly Parser<char, char> AnyCharWithPlaceholder = AnyCharExcept(Constant.FindTemplate.All.Except([Constant.PlaceholderSeparator]));
    internal static readonly Parser<char, string> Placeholder = CommonParser.Identifier.Between(CommonParser.PlaceholderSeparator);
    internal static readonly Parser<char, string> StringWithPlaceholder = AnyCharWithPlaceholder.AtLeastOnceString();
    internal static readonly Parser<char, string> Should = String(Constant.Should);
}