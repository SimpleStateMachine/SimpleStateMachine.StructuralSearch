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
                var parser = parsers.ElementAt(i);
                if (parser is PlaceholderParser placeholderParser)
                {
                    var next = parsers.ElementAt(i + 1) as Parser<char, SourceMatch>;
                    var end = Parser<char>.End.ThenReturn(SourceMatch.Empty);
                    var test1 = parsers.ElementAtOrDefault(i + 2);
                    var test2 = test1 as Parser<char, SourceMatch>;
                    var nextNext = test2 ?? end;
                    var list = new List<Parser<char, SourceMatch>>{next, nextNext };
                    placeholderParser.SetParsers(list);
                    parser = placeholderParser as Parser<TToken, T>;
                }
                
                if (!parser.TryParse(ref state, ref expecteds, out var _result))
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
    }
}