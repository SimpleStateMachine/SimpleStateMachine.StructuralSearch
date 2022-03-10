using System;
using System.Collections.Generic;
using System.Linq;
using Pidgin;

namespace SimpleStateMachine.StructuralSearch
{
    public class SeriesParser<TToken, T, R>: Parser<TToken, R>
    {
        private readonly Func<IEnumerable<T>, R> _getResult;
        private readonly IEnumerable<Parser<TToken, T>> parsers;

        public SeriesParser(IEnumerable<Parser<TToken, T>> parsers, Func<IEnumerable<T>, R> getResult)
        {
            this._getResult = getResult;
            this.parsers = parsers;
        }

        public override bool TryParse(ref ParseState<TToken> state, ref PooledList<Expected<TToken>> expecteds, out R result)
        {
            var results = new List<T>();
            var count = parsers.Count();
            
            for (var i = count-1; i >= 0 ; i--)
            {
                if (parsers.ElementAt(i) is LookaheadParser<TToken, T> lookaheadParser)
                {
                    var number = i;
                    if (number + 2 < count - 1)
                    {
                        lookaheadParser.Lookahead(() => parsers.ElementAt(number + 1), () =>
                            parsers.ElementAt(number + 2));
                    }
                    else
                    {
                        lookaheadParser.Lookahead(() => parsers.ElementAt(number + 1), () =>
                            Parser<TToken>.End);
                    }
              
                }
            }
            
            for (int i = 0; i < parsers.Count(); i++)
            {
                var parser = parsers.ElementAt(i);
                if (!parser.TryParse(ref state, ref expecteds, out var _result))
                {
                    result = default (R);
                    return false;
                }
                
                Console.WriteLine($"R: {_result.ToString()}");
                results.Add(_result);
                
            }
            
            result = _getResult(results);
            return true;
        }
    }
}