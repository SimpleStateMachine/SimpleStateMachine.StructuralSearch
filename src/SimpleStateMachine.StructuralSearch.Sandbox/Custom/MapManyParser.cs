using System;
using System.Collections.Generic;
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
        
        public override bool TryParse(ref ParseState<TToken> state, ref PooledList<Expected<TToken>> expecteds, out R result)
        {
            var results = new List<T>();
            foreach (var parser in parsers)
            {
                if (!parser.TryParse(ref state, ref expecteds, out var _result))
                {
                    result = default (R);
                    return false;
                }
                results.Add(_result);
            }
            
            result = _func(results);
            return true;
        }
    }
}