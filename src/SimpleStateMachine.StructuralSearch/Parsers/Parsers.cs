using System;
using System.Collections.Generic;
using System.Linq;
using Pidgin;
using Pidgin.TokenStreams;
using static Pidgin.Parser;

namespace SimpleStateMachine.StructuralSearch
{
    public static class Parsers
    {
        public static Parser<char, string> Stringc(char character) => String(character.ToString());

        public static Parser<TToken, IEnumerable<T>> MapToMany<TToken, T>(Parser<TToken, T> parser1,
            Parser<TToken, T> parser2, Parser<TToken, T> parser3)
        {
            if (parser1 == null)
                throw new ArgumentNullException(nameof(parser1));
            if (parser2 == null)
                throw new ArgumentNullException(nameof(parser2));
            if (parser3 == null)
                throw new ArgumentNullException(nameof(parser3));

            return Map((arg1, arg2, arg3) => (IEnumerable<T>)new List<T> { arg1, arg2, arg3 }, parser1, parser2,
                parser3);
        }

        public static Parser<TToken, IEnumerable<T>> MapToMany<TToken, T>(Parser<TToken, T> parser1,
            Parser<TToken, IEnumerable<T>> parser2, Parser<TToken, T> parser3)
        {
            if (parser1 == null)
                throw new ArgumentNullException(nameof(parser1));
            if (parser2 == null)
                throw new ArgumentNullException(nameof(parser2));
            if (parser3 == null)
                throw new ArgumentNullException(nameof(parser3));

            return Map((arg1, arg2, arg3) =>
            {
                var result = arg2.ToList();
                result.Insert(0, arg1);
                result.Add(arg3);
                return (IEnumerable<T>)result;
            }, parser1, parser2, parser3);
        }

        public static Parser<TToken, R> Series<TToken, T, R>(IEnumerable<Parser<TToken, T>> parsers,
            Func<IEnumerable<T>, R> func)
        {
            if (parsers == null)
                throw new ArgumentNullException(nameof(parsers));
            if (func == null)
                throw new ArgumentNullException(nameof(func));

            return new SeriesParser<TToken, T, R>(parsers, func);
        }

        public static Parser<char, IEnumerable<T>> BetweenChars<T>(char left, char right,
            Func<char, Parser<char, T>> leftRight,
            Parser<char, IEnumerable<T>> expr)
            => MapToMany(leftRight(left), expr, leftRight(right));


        public static Parser<char, IEnumerable<T>> BetweenOneOfChars<T>(Func<char, Parser<char, T>> leftRight,
            Parser<char, IEnumerable<T>> expr, params (char, char)[] values)
        {
            return OneOf(values.Select(x =>
                MapToMany(leftRight(x.Item1), expr, leftRight(x.Item2)))
            );
        }

        public static Parser<char, IEnumerable<T>> BetweenOneOfChars<T>(Func<char, Parser<char, T>> leftRight,
            Parser<char, T> expr, params (char, char)[] values)
        {
            return OneOf(values.Select(x =>
                MapToMany(leftRight(x.Item1), expr, leftRight(x.Item2)))
            );
        }
    }
}