using Pidgin;
using Pidgin.Expression;
using static Pidgin.Parser;

namespace SimpleStateMachine.StructuralSearch.Sandbox
{
  
    public static class PlaceholderParser
    {


        public static readonly Parser<char, string> Identifier
            = Letter.Then(Common.Symbol.ManyString(), (h, t) => h + t);

        private static Parser<char, T> PlaceholderSeparator<T>(Parser<char, T> parser)
            => parser.Between(String(Common._placeholderSeparator), String(Common._placeholderSeparator));

        public static Parser<char, string> Placeholder()
            => PlaceholderSeparator(Identifier);

    }
}