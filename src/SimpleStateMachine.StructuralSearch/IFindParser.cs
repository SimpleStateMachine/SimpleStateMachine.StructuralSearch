using System.Collections.Generic;
using System.IO;
using Pidgin;
using SimpleStateMachine.StructuralSearch.ReplaceTemplate;

namespace SimpleStateMachine.StructuralSearch
{
    public interface IFindParser
    {
        IEnumerable<FindParserMatch> Parse(ref IParsingContext context);
    }
}