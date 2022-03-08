using System.Collections.Generic;
using Pidgin;
using SimpleStateMachine.StructuralSearch.Sandbox.Extensions;
using static Pidgin.Parser<char>;
using static Pidgin.Parser;
using static SimpleStateMachine.StructuralSearch.Sandbox.Parsers;
using static Pidgin.Parser<char>;

namespace SimpleStateMachine.StructuralSearch.Sandbox
{
    public static class CommonTemplateParser
    {
        public static readonly Parser<char, string> Placeholder
            = CommonParser.Identifier.Between(Char(Constant.PlaceholderSeparator));
        
        public static readonly Parser<char, string> StringWithoutParenthesisedAndWhiteSpaces
            = AnyCharExcept(Constant.AllExclude(Constant.PlaceholderSeparator))
                .AtLeastOnceString();
        
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