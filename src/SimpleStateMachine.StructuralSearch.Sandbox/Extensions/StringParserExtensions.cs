using System;
using System.Collections.Generic;
using Pidgin;

namespace SimpleStateMachine.StructuralSearch.Sandbox.Extensions
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
        // public static Parser<TToken, List<string>> ToMany<TToken>(this Parser<TToken, string> parser)
        // {
        //     return parser.Select(x => new List<string>{ x });
        // }
    }
}