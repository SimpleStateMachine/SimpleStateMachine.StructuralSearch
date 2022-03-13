using System;
using Pidgin;
using SimpleStateMachine.StructuralSearch.Extensions;
using static Pidgin.Parser;
namespace SimpleStateMachine.StructuralSearch
{
    public static class CommonParser
    {
        public static readonly Parser<char, string> Empty
            = Parsers.String(Constant.Empty, false);
        
        public static readonly Parser<char, char> AnyChar
            = AnyCharExcept(Constant.FindTemplate.All());
        
        public static readonly Parser<char, char> Space
            = Char(Constant.Space);
        
        public static readonly Parser<char, string> AnyString
            = AnyChar.AtLeastOnceString();
        
        public static readonly Parser<char, string> Spaces
            = Space.AtLeastOnceString();
        
        public static readonly Parser<char, string> LineEnds
            = EndOfLine.AtLeastOnceString();
        
        public static readonly Parser<char, string> WhiteSpaces
            = OneOf(Spaces, LineEnds);

        public static readonly Parser<char, string> Identifier
            = Letter.Then(AnyString, (h, t) => h + t);
        
        public static readonly Parser<char, char> Comma
            = Char(Constant.Comma);
        
        public static readonly Parser<char, char> DoubleQuotes
            = Char(Constant.DoubleQuotes);
        
        public static readonly Parser<char, char> SingleQuotes
            = Char(Constant.SingleQuotes);

        public static Parser<char, T> Parenthesised<T>(Parser<char, T> parser, Func<Parser<char, string>, Parser<char, string>> custom)
        {
            return parser.Between(custom(Parsers.Stringc(Constant.LeftParenthesis)),
                custom(Parsers.Stringc(Constant.RightParenthesis)));
        }
    }
}