using System.Collections.Generic;
using Pidgin;
using SimpleStateMachine.StructuralSearch.ReplaceTemplate;

namespace SimpleStateMachine.StructuralSearch
{
    public interface IFindParser
    {
        SourceMatch Parse(ref IParsingContext context, string input);
    }
}