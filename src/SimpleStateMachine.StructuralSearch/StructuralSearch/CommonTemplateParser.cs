using Pidgin;
using SimpleStateMachine.StructuralSearch.Extensions;
using static Pidgin.Parser<char>;
using static Pidgin.Parser;

namespace SimpleStateMachine.StructuralSearch
{
    internal static class CommonTemplateParser
    {
        internal static readonly Parser<char, char> AnyCharWithPlshd =
            AnyCharExcept(Constant.FindTemplate.AllExclude(Constant.PlaceholderSeparator));
        
        internal static readonly Parser<char, string> Placeholder
            = CommonParser.Identifier.Between(Char(Constant.PlaceholderSeparator));
        
        internal static readonly Parser<char, string> StringWithPlshd
            = AnyCharWithPlshd.AtLeastOnceString();
        
        //can be contains one $
        internal static readonly Parser<char, string> StringWithoutPlaceholder
            = Any.AtLeastOnceAsStringUntilNot(Placeholder);

        internal static readonly Parser<char, string> Token
            = OneOf(Placeholder.Try(),
                CommonParser.WhiteSpaces.Try(),
                ParsingConfiguration.Comment.Try(),
                CommonParser.AnyString.Try());
    }
}