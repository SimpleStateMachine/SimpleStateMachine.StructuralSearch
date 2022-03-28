using System;
using System.Collections.Generic;
using System.Linq;
using Pidgin;

namespace SimpleStateMachine.StructuralSearch
{
    public class SeriesParser<TToken, T, R>: Parser<TToken, R>
    {
        private readonly Func<IEnumerable<T>, R> _getResult;
        private readonly IEnumerable<Parser<TToken, T>> _parsers;
        private readonly ParsingContext _context;
        private bool _initialized = false;
        public SeriesParser(ParsingContext context, IEnumerable<Parser<TToken, T>> parsers, Func<IEnumerable<T>, R> getResult)
        {
            _getResult = getResult;
            _parsers = parsers;
            _context = context;
        }
    
        public override bool TryParse(ref ParseState<TToken> state, ref PooledList<Expected<TToken>> expecteds, out R result)
        {
            Fill();
            var results = new List<T>();
            var count = _parsers.Count();
            
            for (int i = 0; i < _parsers.Count(); i++)
            {
                var parser = _parsers.ElementAt(i);
                if (!parser.TryParse(ref state, ref expecteds, out var _result))
                {
                    result = default (R);
                    return false;
                }
                results.Add(_result);
                
                SkipLookedParsers(parser, ref state);

                void SkipLookedParsers(Parser<TToken, T> parser, ref ParseState<TToken> state)
                {
                    if (parser is not ParserWithLookahead<TToken, T> lookaheadParser || lookaheadParser is { OnLookahead: null }) 
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
            
            result = _getResult(results);
            return true;
        }

        
        private void Fill()
        {
            if(_initialized)
                return;
            
            var count = _parsers.Count();
            
            for (var i = count-1; i >= 0 ; i--)
            {
                var parser = _parsers.ElementAt(i);
                if (parser is ParserWithLookahead<TToken, T> lookaheadParser)
                {
                    var number = i;
                    lookaheadParser.Lookahead(() => _parsers.ElementAtOrDefault(number + 1), () =>
                        _parsers.ElementAtOrDefault(number + 2));
                }
                
                if (parser is IContextDependent element)
                {
                    element.SetContext(_context);
                }
            }

            _initialized = true;
        }
    }
}