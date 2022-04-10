﻿using System;
using System.Collections.Generic;
using System.Linq;
using Pidgin;

namespace SimpleStateMachine.StructuralSearch
{
    public class SeriesParser : Parser<char, IEnumerable<string>>, IContextDependent
    {
        public IEnumerable<Parser<char, string>> Parsers { get; }
        
        public SeriesParser(IEnumerable<Parser<char, string>> parsers)
        {
            Parsers = parsers;
            
            InitializeLookaheadParsers();
        }

        public override bool TryParse(ref ParseState<char> state, ref PooledList<Expected<char>> expecteds,
            out IEnumerable<string> result)
        {
            var results = new List<string>();
            var count = Parsers.Count();

            for (int i = 0; i < count; i++)
            {
                var parser = Parsers.ElementAt(i);
                if (!parser.TryParse(ref state, ref expecteds, out var _result))
                {
                    result = Enumerable.Empty<string>();
                    return false;
                }

                results.Add(_result);

                SkipLookedParsers(parser, ref state);

                void SkipLookedParsers(Parser<char, string> parser, ref ParseState<char> state)
                {
                    if (parser is not ParserWithLookahead<char, string> lookaheadParser ||
                        lookaheadParser is { OnLookahead: null })
                        return;

                    var lookaheadResults = lookaheadParser.OnLookahead.Invoke();

                    foreach (var result in lookaheadResults)
                    {
                        results.Add(result.Result);
                        state.Advance(result.TokensCount);
                        i++;
                    }

                    //recursion
                    foreach (var result in lookaheadResults)
                    {
                        SkipLookedParsers(result.Parser, ref state);
                    }
                }
            }

            result = results;
            return true;
        }
        
        public void SetContext(IParsingContext parsingContext)
        {
            foreach (var parser in Parsers)
            {
                if (parser is IContextDependent element)
                {
                    element.SetContext(parsingContext);
                }
            }
        }
        
        private void InitializeLookaheadParsers()
        {
            var count = Parsers.Count();
            
            for (var i = count-1; i >= 0 ; i--)
            {
                var parser = Parsers.ElementAt(i);
                if (parser is ParserWithLookahead<char, string> lookaheadParser)
                {
                    var number = i;
                    lookaheadParser.Lookahead(() => Parsers.ElementAtOrDefault(number + 1), () =>
                        Parsers.ElementAtOrDefault(number + 2));
                }
            }
        }
    }
}