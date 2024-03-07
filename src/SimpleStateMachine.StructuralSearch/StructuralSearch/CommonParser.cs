using System;
using Pidgin;
using static Pidgin.Parser;
namespace SimpleStateMachine.StructuralSearch
{
    public static class CommonParser
    {
        internal static readonly Parser<char, string> Empty
            = Parsers.String(Constant.EmptyString, false);
        
        internal static readonly Parser<char, char> AnyChar
            = AnyCharExcept(Constant.FindTemplate.All);
        
        internal static readonly Parser<char, char> Space
            = Char(Constant.Space);
        
        public static readonly Parser<char, Unit> EOF
            = Parser<char>.End;

        internal static readonly Parser<char, string> AnyString
            = AnyChar.AtLeastOnceString();
        
        internal static readonly Parser<char, string> Spaces
            = Space.AtLeastOnceString();
        
        internal static readonly Parser<char, string> LineEnds
            = EndOfLine.AtLeastOnceString();
        
        internal static readonly Parser<char, string> WhiteSpaces
            = OneOf(Spaces, LineEnds, LineEnds)
                .AtLeastOnceString();

        internal static readonly Parser<char, string> Identifier
            = Letter.Then(AnyString, (h, t) => h + t);
        
        internal static readonly Parser<char, char> Comma
            = Char(Constant.Comma);
        
        internal static readonly Parser<char, char> Colon
            = Char(Constant.Colon);
        
        internal static readonly Parser<char, char> DoubleQuotes
            = Char(Constant.DoubleQuotes);
        
        internal static readonly Parser<char, char> SingleQuotes
            = Char(Constant.SingleQuotes);
        
        internal static readonly Parser<char, char> Dote
            = Char(Constant.Dote);
        
        internal static readonly Parser<char, char> Underscore
            = Char(Constant.Underscore);

        internal static Parser<char, T> Parenthesised<T, TResult>(Parser<char, T> parser, Func<char, Parser<char, TResult>> custom)
            => parser.Between
        (
            custom(Constant.LeftParenthesis),
            custom(Constant.RightParenthesis)
        );

        internal static Parser<char, char> Escaped(params char [] chars)
            => Char(Constant.BackSlash).Then(OneOf(chars));
    }
}