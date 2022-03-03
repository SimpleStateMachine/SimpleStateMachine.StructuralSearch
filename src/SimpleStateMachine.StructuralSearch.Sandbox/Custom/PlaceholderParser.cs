using System;
using System.Collections.Generic;
using Pidgin;

namespace SimpleStateMachine.StructuralSearch.Sandbox.Custom
{
    public class PlaceholderParser: Parser<char, SourceMatch>
    {
        private Func<IEnumerable<Parser<char, SourceMatch>>, Parser<char, SourceMatch>> _parser;
        public string Name { get; }
        private IEnumerable<Parser<char, SourceMatch>> _parsers;
        public PlaceholderParser(string name, Func<IEnumerable<Parser<char, SourceMatch>>, Parser<char, SourceMatch>>  parser)
        {
            Name = name;
            _parser = parser;
        }

        public void SetParsers(IEnumerable<Parser<char, SourceMatch>> parsers)
        {
            _parsers = parsers;
        }
        public override bool TryParse(ref ParseState<char> state, ref PooledList<Expected<char>> expecteds, out SourceMatch result)
        { 
            var isCorrect = _parser(_parsers).TryParse(ref state, ref expecteds, out result);
            
            return isCorrect;
        }
    }
}