using Pidgin;
using SimpleStateMachine.StructuralSearch.Extensions;

namespace SimpleStateMachine.StructuralSearch.Templates.FindTemplate
{
    public static class ParserToParser
    {
        public static Parser<char, Parser<char, string>> String(string token, bool ignoreCase = false)
        {
            return Parsers.Parsers.String(token, ignoreCase).Select(x => Parsers.Parsers.String(token, ignoreCase));
        }
        
        public static Parser<char, Parser<char, string>> Stringc(char token, bool ignoreCase = false)
        {
            var _token = token.ToString();
            return Parsers.Parsers.String(_token, ignoreCase).Select(x => Parsers.Parsers.String(_token, ignoreCase));
        }

        public static Parser<char, Parser<char, SourceMatch>> StringcMatch(char token, bool ignoreCase = false)
        {
            var _token = token.ToString();
            return Parsers.Parsers.String(_token, ignoreCase).Select(x => Parsers.Parsers.String(_token, ignoreCase).AsMatch());
        }

        public static Parser<char, Parser<char, string>> ResultAsParser(Parser<char, string> parser, bool ignoreCase = false)
        {
            return parser.Select(value => Parsers.Parsers.String(value, ignoreCase));
        }
        
        public static Parser<char, Parser<char, SourceMatch>> ResultAsMatch(Parser<char, string> parser, bool ignoreCase = false)
        {
            return parser.Select(x=> Parsers.Parsers.String(x, ignoreCase).AsMatch());
        }
        

    }
}