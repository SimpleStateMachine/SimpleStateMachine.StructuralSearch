using System.Collections.Generic;
using System.IO;
using Pidgin;
using SimpleStateMachine.StructuralSearch.ReplaceTemplate;

namespace SimpleStateMachine.StructuralSearch
{
    public interface IFindParser
    {
        SourceMatch Parse(ref IParsingContext context, IInput input);
    }
}