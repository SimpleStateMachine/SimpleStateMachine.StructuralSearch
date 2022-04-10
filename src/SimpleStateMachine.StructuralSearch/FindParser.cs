using System.Collections.Generic;
using Pidgin;
using SimpleStateMachine.StructuralSearch.Extensions;

namespace SimpleStateMachine.StructuralSearch
{
    public class FindParser : IFindParser
    {
        private SeriesParser Parser { get; }
        public FindParser(SeriesParser parser)
        {
            Parser = parser;
        }

        public SourceMatch Parse(IParsingContext context, string input)
        {
            Parser.SetContext(context);
            var result = Parser.Select(x => string.Join(string.Empty, x)).AsMatch().Parse(input);
            
            return result.Success ? result.Value : SourceMatch.Empty;
        }
    }
}