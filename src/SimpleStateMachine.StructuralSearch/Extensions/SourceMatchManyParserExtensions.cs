﻿using System;
using System.Collections.Generic;
using System.Linq;
using Pidgin;

namespace SimpleStateMachine.StructuralSearch.Extensions
{
    public static class SourceMatchParserExtensions
    {
        // public static Parser<TToken, Parser<TToken, R>> JoinResults<TToken, T, R>(this Parser<TToken, IEnumerable<Parser<TToken, T>>> parsers)
        // {
        //     return parsers.Select(x => Parsers.Series(x, getResult));
        // }
        public static Parser<char, Parser<char, string>> JoinResults(this Parser<char, IEnumerable<Parser<char, string>>> parsers)
        {
            return parsers.Select(x => Parsers.Series(x, y => string.Join(string.Empty, y)));
        }
        public static Parser<char, Parser<char, SourceMatch>> JoinResults(this Parser<char, IEnumerable<Parser<char, SourceMatch>>> parsers)
        {
            return parsers.Select(x => Parsers.Series(x, y => y.Concatenate()));
        }
    }
}