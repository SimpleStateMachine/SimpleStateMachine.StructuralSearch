using System;
using System.Collections.Generic;
using Pidgin;
using static Pidgin.Parser<char>;
using static Pidgin.Parser;

namespace SimpleStateMachine.StructuralSearch.Extensions
{
    public static class StringParserExtensions
    {
        public static Parser<TToken, string> BetweenAsThen<TToken>(this Parser<TToken, string> parser,
            Parser<TToken, char> parser1,
            Parser<TToken, char> parser2)
        {
            if (parser1 == null)
                throw new ArgumentNullException(nameof(parser1));
            if (parser2 == null)
                throw new ArgumentNullException(nameof(parser2));

            return Parser.Map<TToken, char, string, char, string>(
                (before, str, after) => before + str + after,
                parser1, parser, parser2);
        }
        
        public static Parser<TToken, string> BetweenAsThen<TToken>(this Parser<TToken, string> parser,
            Parser<TToken, string> parser1,
            Parser<TToken, string> parser2)
        {
            if (parser1 == null)
                throw new ArgumentNullException(nameof(parser1));
            if (parser2 == null)
                throw new ArgumentNullException(nameof(parser2));

            return Parser.Map<TToken, string, string, string, string>(
                (before, str, after) => before + str + after,
                parser1, parser, parser2);
        }
        
        public static Parser<TToken, IEnumerable<string>> ToMany<TToken>(this Parser<TToken, string> parser)
        {
            return parser.Select(x => new List<string>() { x }).ToIEnumerable();
        }
        
        public static Parser<TToken, string> JoinToString<TToken>(this Parser<TToken, IEnumerable<string>> parser, string separator = null)
        {
            separator ??= string.Empty;
            return parser.Select(x => string.Join(separator, x));
        }
        
        public static Parser<TToken, SourceMatch> AsMatch<TToken>(this Parser<TToken, string> parser)
        {
            return parser.Then(Parser<TToken>.CurrentOffset, (s, offset) => new SourceMatch(s, offset - s.Length, offset));
        }
    }
}