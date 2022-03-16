using System;
using Pidgin;
using static Pidgin.Parser;
namespace SimpleStateMachine.StructuralSearch
{
    internal static class CommonParser
    {
        internal static readonly Parser<char, string> Empty
            = Parsers.String(Constant.Empty, false);
        
        internal static readonly Parser<char, char> AnyChar
            = AnyCharExcept(Constant.FindTemplate.All());
        
        internal static readonly Parser<char, char> Space
            = Char(Constant.Space);
        
        internal static readonly Parser<char, string> AnyString
            = AnyChar.AtLeastOnceString();
        
        internal static readonly Parser<char, string> Spaces
            = Space.AtLeastOnceString();
        
        internal static readonly Parser<char, string> LineEnds
            = EndOfLine.AtLeastOnceString();
        
        internal static readonly Parser<char, string> WhiteSpaces
            = OneOf(Spaces, LineEnds);

        internal static readonly Parser<char, string> Identifier
            = Letter.Then(AnyString, (h, t) => h + t);
        
        internal static readonly Parser<char, char> Comma
            = Char(Constant.Comma);
        
        internal static readonly Parser<char, char> DoubleQuotes
            = Char(Constant.DoubleQuotes);
        
        internal static readonly Parser<char, char> SingleQuotes
            = Char(Constant.SingleQuotes);
        
        internal static readonly Parser<char, char> Dote
            = Char(Constant.Dote);

        internal static Parser<char, T> Parenthesised<T>(Parser<char, T> parser, Func<Parser<char, string>, Parser<char, string>> custom)
        {
            return parser.Between(custom(Parsers.Stringc(Constant.LeftParenthesis)),
                custom(Parsers.Stringc(Constant.RightParenthesis)));
        }
    }
}