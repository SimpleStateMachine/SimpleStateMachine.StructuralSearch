using SimpleStateMachine.StructuralSearch.Context;

namespace SimpleStateMachine.StructuralSearch.Parsers;

internal interface IContextDependent
{
    void SetContext(ref IParsingContext context);
}