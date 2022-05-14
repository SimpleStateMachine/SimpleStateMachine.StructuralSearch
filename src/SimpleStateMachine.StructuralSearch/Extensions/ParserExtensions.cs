using System;
using Pidgin;
using System.Collections.Generic;
using static Pidgin.Parser;

namespace SimpleStateMachine.StructuralSearch.Extensions
{
    public static class ParserExtensions
    {
        public static Parser<TToken, T> Try<TToken, T>(this Parser<TToken, T> parser)
        {
            return Parser.Try(parser);
        }
        
        public static Parser<TToken, IEnumerable<T>> AsMany<TToken, T>(this Parser<TToken, T> parser)
        {
            return parser.Select(x => (IEnumerable<T>)new List<T> { x });
        }

        public static Parser<TToken, IEnumerable<T>> AtLeastOnceUntilNot<TToken, T, U>(this Parser<TToken, T> parser,
            Parser<TToken, U> terminator)
        {
            return parser != null
                ? parser.AtLeastOnceUntil(Not(terminator))
                : throw new ArgumentNullException(nameof(parser));
        }
        
        public static Parser<TToken, IEnumerable<T>> UntilNot<TToken, T, U>(this Parser<TToken, T> parser,
            Parser<TToken, U> terminator)
        {
            return parser != null
                ? parser.Until(Not(terminator))
                : throw new ArgumentNullException(nameof(parser));
        }
        
        public static bool TryParse(this Parser<char, string> parser, string value, out string? result)
        {
            if(parser is null)
                throw new ArgumentNullException(nameof(parser));
            
            var res = parser.Parse(value);
            result = res.Success ? res.Value : default;
            return res.Success;
        }
        
        public static Parser<TToken, T> ThenInvoke<TToken, T>(this Parser<TToken, T> parser, Action<T> action)
        {
            return parser.Select(x =>
            {
                action.Invoke(x);
                return x;
            });
        }
        
        public static Parser<TToken, bool> Contains<TToken, T>(this Parser<TToken, T> parser)
        {
            return parser != null
                ? parser.Optional().Select(x => x.HasValue)
                : throw new ArgumentNullException(nameof(parser));
        }
        
        public static Parser<char, Match<T>> Match<T>(this Parser<char, T> parser)
        {
            return Map((oldPos, oldOffset, result, newPos, newOffset) =>
                {
                    var line = new LinePosition(oldPos.Line, newPos.Line);
                    var column = new ColumnPosition(oldPos.Col, newPos.Col);
                    var offset = new OffsetPosition(oldOffset, newOffset);
                    var lenght = newOffset - oldOffset;
                    return new Match<T>(result, lenght, column, line, offset);
                },
                Parser<char>.CurrentPos, Parser<char>.CurrentOffset,
                parser,
                Parser<char>.CurrentPos, Parser<char>.CurrentOffset);
        }

        public static Parser<TToken, T> WithDebug<TToken, T>(this Parser<TToken, T> parser, string label)
        {
            return Map((u, t, v) =>
            {
                Console.WriteLine($"{label} ({t.Col}) : {u} ");
                return u;
            }, parser, Parser<TToken>.CurrentPos, Parser<TToken>.CurrentSourcePosDelta);
        }

        public static Parser<TToken, T> WithDebug<TToken, T>(this Parser<TToken, T> parser)
        {
            return Map((u, t, v) =>
            {
                Console.WriteLine($"({t.Col}) : {u} ");
                return u;
            }, parser, Parser<TToken>.CurrentPos, Parser<TToken>.CurrentSourcePosDelta);
        }

        public static Parser<TToken, T> BetweenAsThen<TToken, T, U, V>(this Parser<TToken, T> parser,
            Parser<TToken, U> parser1,
            Parser<TToken, V> parser2, Func<U, T, V, T> func)
        {
            if (parser1 == null)
                throw new ArgumentNullException(nameof(parser1));
            if (parser2 == null)
                throw new ArgumentNullException(nameof(parser2));
            return Parser.Map<TToken, U, T, V, T>(func, parser1, parser, parser2);
        }
        
        public static Parser<TToken, R> As<TToken, T, R>(this Parser<TToken, T> parser)
            where T: R
        {
            return parser.Select(x => (R)x);
        }
        
        public static Parser<TToken, T> After<TToken, T, U>(this Parser<TToken, T> parser, Parser<TToken, U> parserAfter)
        {
            return parserAfter.Then(parser, (u, t) => t);
        }
        
        public static Parser<char, T> ParenthesisedOptional<T>(this Parser<char, T> parser, Func<Parser<char, string>, Parser<char, string>> custom)
        {
            return OneOf(CommonParser.Parenthesised(parser, custom).Try(), parser);
        }
    }
}