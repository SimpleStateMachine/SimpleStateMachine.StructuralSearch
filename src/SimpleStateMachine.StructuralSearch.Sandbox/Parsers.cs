﻿using System;
using System.Collections.Generic;
using System.Linq;
using Pidgin;
using Pidgin.TokenStreams;
using SimpleStateMachine.StructuralSearch.Sandbox.Custom;
using static Pidgin.Parser;

namespace SimpleStateMachine.StructuralSearch.Sandbox
{
    public static class Parsers
    {
        public static Parser<char, string> Stringc(char character) => String(character.ToString());
        
        public static Parser<TToken, List<T>> MapToMany<TToken, T>(Parser<TToken, T> parser1, Parser<TToken, T> parser2, Parser<TToken, T> parser3)
        {
            if (parser1 == null)
                throw new ArgumentNullException(nameof(parser1));
            if (parser2 == null)
                throw new ArgumentNullException(nameof(parser2));
            if (parser3 == null)
                throw new ArgumentNullException(nameof(parser2));
            
            return Map((arg1, arg2, arg3) => new List<T>{arg1, arg2, arg3}, parser1, parser2, parser2);
        }
        
        public static Parser<TToken, IEnumerable<T>> MapToMany<TToken, T>(Parser<TToken, T> parser1, Parser<TToken, IEnumerable<T>> parser2, Parser<TToken, T> parser3)
        {
            if (parser1 == null)
                throw new ArgumentNullException(nameof(parser1));
            if (parser2 == null)
                throw new ArgumentNullException(nameof(parser2));
            if (parser3 == null)
                throw new ArgumentNullException(nameof(parser2));
            
            return Map((arg1, arg2, arg3) =>
            {
                var result = arg2.ToList();
                result.Insert(0, arg1);
                result.Add(arg3);
                return result as IEnumerable<T>;
            }, parser1, parser2, parser3);
        }


        public static Parser<TToken, R> Series<TToken, T, R>(IEnumerable<Parser<TToken, T>> parsers, Func<IEnumerable<T>, R> func)
        {
            if (parsers == null)
                throw new ArgumentNullException(nameof (parsers));
            if (func == null)
                throw new ArgumentNullException(nameof (func));

            return new SeriesParser<TToken, T, R>(parsers, func);
        }

    }
}