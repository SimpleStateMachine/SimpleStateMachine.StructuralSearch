using System;
using System.Collections.Generic;
using System.Linq;
using Pidgin;

namespace SimpleStateMachine.StructuralSearch.Sandbox.Custom
{
    public class SeriesParser<TToken, T, R>: Parser<TToken, R>
    {
        private readonly Func<IEnumerable<T>, R> _func;
        private readonly IEnumerable<Parser<TToken, T>> parsers;

        public SeriesParser(IEnumerable<Parser<TToken, T>> parsers, Func<IEnumerable<T>, R> func)
        {
            this._func = func;
            this.parsers = parsers;
        }
        
        // public override bool TryParse(ref ParseState<TToken> state, ref PooledList<Expected<TToken>> expecteds, out R result)
        // {
        //     var results = new List<T>();
        //     foreach (var parser in parsers)
        //     {
        //         if (!parser.TryParse(ref state, ref expecteds, out var _result))
        //         {
        //             result = default (R);
        //             return false;
        //         }
        //         Console.WriteLine($"R: {_result}");
        //         results.Add(_result);
        //     }
        //     
        //     result = _func(results);
        //     return true;
        // }
        
        
        public override bool TryParse(ref ParseState<TToken> state, ref PooledList<Expected<TToken>> expecteds, out R result)
        {
            var results = new List<T>();
            for (int i = 0; i < parsers.Count(); i++)
            {
                T _result;
                var parser = parsers.ElementAt(i);
                
                if (parser is ILookaheadParser<TToken, T> lookaheadParser)
                {
                    var next = parsers.ElementAt(i + 1);
                    var nextNext = parsers.ElementAtOrDefault(i + 2);
                    if (!TryParseWithLookahead(lookaheadParser, next, nextNext, ref state, ref expecteds, out _result))
                    {
                        result = default (R);
                        return false;
                    }
                }
                else if (!parser.TryParse(ref state, ref expecteds, out _result))
                {
                    result = default (R);
                    return false;
                }
                
                Console.WriteLine($"R: {_result.ToString()}");
                results.Add(_result);
                
            }
            
            result = _func(results);
            return true;
        }

        private bool TryParseWithLookahead(ILookaheadParser<TToken, T> lookaheadParser, 
            Parser<TToken, T> next, 
            Parser<TToken, T> nextNext, 
            ref ParseState<TToken> state, 
            ref PooledList<Expected<TToken>> expected,
            out T result)
        {
            
            if (nextNext is not null)
            {
                return lookaheadParser.TryParse(next, nextNext, ref state, ref expected, out result);
            }
           
            return lookaheadParser.TryParse(next, Parser<TToken>.End, ref state, ref expected, out result);
        }
    }
}