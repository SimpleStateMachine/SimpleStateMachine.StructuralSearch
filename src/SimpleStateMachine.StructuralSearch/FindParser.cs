using System.Collections.Generic;
using System.IO;
using Pidgin;
using SimpleStateMachine.StructuralSearch.Extensions;
using YamlDotNet.Core.Events;

namespace SimpleStateMachine.StructuralSearch
{
    public class FindParser : IFindParser
    {
        private SeriesParser Parser { get; }
        public FindParser(SeriesParser parser)
        {
            Parser = parser;
        }

        public SourceMatch Parse(ref IParsingContext context, IInput input)
        {
            Parser.SetContext(ref context);
            
            var result = input.Parse(Parser.Select(x => string.Join(string.Empty, x))
                .AsMatch());

            return result.Success ? result.Value : SourceMatch.Empty;
        }
    }
}