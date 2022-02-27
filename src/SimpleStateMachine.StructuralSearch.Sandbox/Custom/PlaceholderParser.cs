using Pidgin;

namespace SimpleStateMachine.StructuralSearch.Sandbox.Custom
{
    public class PlaceholderParser: Parser<char, SourceMatch>
    {
        private Parser<char, SourceMatch> _parser;
        private string _name;
        public PlaceholderParser(string name, Parser<char, SourceMatch> parser)
        {
            _name = name;
            _parser = parser;
        }
        public override bool TryParse(ref ParseState<char> state, ref PooledList<Expected<char>> expecteds, out SourceMatch result)
        { 
            var isCorrect = _parser.TryParse(ref state, ref expecteds, out result);
            
            return isCorrect;
        }
    }
}