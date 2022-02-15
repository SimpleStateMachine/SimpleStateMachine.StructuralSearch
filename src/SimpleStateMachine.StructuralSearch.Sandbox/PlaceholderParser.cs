using Pidgin;
using Pidgin.Expression;
using static Pidgin.Parser;

namespace SimpleStateMachine.StructuralSearch.Sandbox
{
  
    public static class PlaceholderParser
    {
        static string  _placeholderSeparator = "$";
        static char  _underScore = '_';

        private static readonly Parser<char, string> Identifier
            = Letter.Then(LetterOrDigit.Or(Char(_underScore)).ManyString(), (h, t) => h + t);

        private static Parser<char, T> PlaceholderSeparator<T>(Parser<char, T> parser)
            => parser.Between(String(_placeholderSeparator), String(_placeholderSeparator));
        
        public static Parser<char, string> Placeholder()
            => PlaceholderSeparator(Identifier);
        
    }
}