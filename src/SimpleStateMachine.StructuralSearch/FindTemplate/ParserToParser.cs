using System;
using System.Collections.Generic;
using System.Linq;
using Pidgin;
using SimpleStateMachine.StructuralSearch.Extensions;
using static Pidgin.Parser<char>;
using static Pidgin.Parser;

namespace SimpleStateMachine.StructuralSearch
{
    public static class ParserToParser
    {
        public static Parser<char, Parser<char, string>> String(string token)
        {
            return Parser.String(token).Select(x => Parser.String(token));
        }
        
        public static Parser<char, Parser<char, string>> Stringc(char token)
        {
            var _token = token.ToString();
            return Parser.String(_token).Select(x => Parser.String(_token));
        }
        
        public static Parser<char, Parser<char, SourceMatch>> StringcMatch(char token)
        {
            var _token = token.ToString();
            return Parser.String(_token).Select(x => Parser.String(_token).AsMatch());
        }

        public static Parser<char, Parser<char, string>> ResultAsParser(Parser<char, string> parser)
        {
            return parser.Select(Parser.String);
        }
        
        public static Parser<char, Parser<char, SourceMatch>> ResultAsMatch(Parser<char, string> parser)
        {
            return parser.Select(x=> Parser.String(x).AsMatch());
        }

    }
}