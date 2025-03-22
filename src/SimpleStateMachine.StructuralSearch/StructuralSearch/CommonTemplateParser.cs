using System.Linq;
using Pidgin;
using SimpleStateMachine.StructuralSearch.Extensions;
using static Pidgin.Parser<char>;
using static Pidgin.Parser;

namespace SimpleStateMachine.StructuralSearch.StructuralSearch;

internal static class CommonTemplateParser
{
    internal static readonly Parser<char, char> AnyCharWithPlaceholder =
        AnyCharExcept(Constant.FindTemplate.All.Except([Constant.PlaceholderSeparator]));
        
    internal static readonly Parser<char, string> Placeholder
        = CommonParser.Identifier.Between(Char(Constant.PlaceholderSeparator));
        
    internal static readonly Parser<char, string> StringWithPlaceholder
        = AnyCharWithPlaceholder.AtLeastOnceString();
        
    internal static readonly Parser<char, string> Should
        = String(Constant.Should);
        
    //can be contains one $
    internal static readonly Parser<char, string> StringWithoutPlaceholder
        = Any.AtLeastOnceAsStringUntilNot(Placeholder);

    internal static readonly Parser<char, string> Token
        = OneOf(Placeholder.Try(),
            CommonParser.WhiteSpaces.Try(),
            CommonParser.AnyString.Try());
}