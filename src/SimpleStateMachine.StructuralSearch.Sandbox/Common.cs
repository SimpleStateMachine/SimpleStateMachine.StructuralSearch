using System;
using Pidgin;
using static Pidgin.Parser;
namespace SimpleStateMachine.StructuralSearch.Sandbox
{
    public static class Common
    {
        public static string  _placeholderSeparator = "$";
        public static char  _underScore = '_';

        
        public static readonly Parser<char, char> Symbol
            = LetterOrDigit.Or(Char(_underScore));
        public static Parser<char, T> Tok<T>(Parser<char, T> token)
            => Try(token).Before(SkipWhitespaces);
        static Parser<char, string> Name(Parser<char, char> firstLetter)
            => Tok(
                from first in firstLetter
                from rest in OneOf(Letter, Digit, Char('_')).ManyString()
                select first + rest
            );
        // public static Parser<char, string> AnyString { get; } = Parser<string>(_ => true);
    }
}