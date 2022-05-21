using Pidgin;
using SimpleStateMachine.StructuralSearch.Extensions;

namespace SimpleStateMachine.StructuralSearch
{
    public static class ParserToParser
    {
        public static Parser<char, Parser<char, char>> CIChar(char token)
        {
            return Parser.CIChar(token).Select(Parser.CIChar);
        }
        
        // public static Parser<char, Parser<char, string>> String(string token, bool ignoreCase = false)
        // {
        //     return Parsers.String(token, ignoreCase).Select(x => Parsers.String(token, ignoreCase));
        // }
        
        // public static Parser<char, Parser<char, string>> Stringc(char token, bool ignoreCase = false)
        // {
        //     var _token = token.ToString();
        //     return Parser.Char(_token, ignoreCase).Select(x => Parsers.String(_token, ignoreCase));
        // }

        public static Parser<char, Parser<char, string>> ResultAsParser(Parser<char, string> parser, bool ignoreCase = false)
        {
            return parser.Select(value => Parsers.String(value, ignoreCase));
        }
        
        public static Parser<char, Parser<char, string>> ParserAsParser(Parser<char, string> parser)
        {
            return parser.Select(value => parser);
        }
        
        public static Parser<char, Parser<char, SourceMatch>> ResultAsMatch(Parser<char, string> parser, bool ignoreCase = false)
        {
            return parser.Select(x=> Parsers.String(x, ignoreCase).AsMatch());
        }
        

    }
}