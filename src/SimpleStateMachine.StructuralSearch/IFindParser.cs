using System.Collections.Generic;

namespace SimpleStateMachine.StructuralSearch
{
    public interface IFindParser
    {
        IEnumerable<FindParserResult> Parse(ref IParsingContext context);
    }
}