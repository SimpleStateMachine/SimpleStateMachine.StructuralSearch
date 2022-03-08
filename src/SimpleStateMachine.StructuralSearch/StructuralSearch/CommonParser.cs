using System;
using Pidgin;
using static Pidgin.Parser;
namespace SimpleStateMachine.StructuralSearch
{
    public static class CommonParser
    {
        public static readonly Parser<char, char> AnyChar
            = AnyCharExcept(Constant.All());
        
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
    }
}