using Pidgin;
using SimpleStateMachine.StructuralSearch.Extensions;
using static Pidgin.Parser<char>;
using static Pidgin.Parser;

namespace SimpleStateMachine.StructuralSearch
{
    public static class CommonTemplateParser
    {
        public static readonly Parser<char, char> AnyCharWithPlshd =
            AnyCharExcept(Constant.FindTemplate.AllExclude(Constant.PlaceholderSeparator));
        
        public static readonly Parser<char, string> Placeholder
            = CommonParser.Identifier.Between(Char(Constant.PlaceholderSeparator));
        
        public static readonly Parser<char, string> StringWithPlshd
            = AnyCharWithPlshd.AtLeastOnceString();
        
        //can be contains one $
        public static readonly Parser<char, string> StringWithoutPlaceholder
            = Any.AtLeastOnceAsStringUntilNot(Placeholder);

        public static readonly Parser<char, string> Token
            = OneOf(Placeholder.Try(),
                CommonParser.WhiteSpaces.Try(),
                ParsingConfiguration.Comment.Try(),
                CommonParser.AnyString.Try());
    }
}