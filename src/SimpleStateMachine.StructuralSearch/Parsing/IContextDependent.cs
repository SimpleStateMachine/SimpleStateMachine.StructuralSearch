using SimpleStateMachine.StructuralSearch.Context;

namespace SimpleStateMachine.StructuralSearch.Parsing;

internal interface IContextDependent
{
    void SetContext(ref IParsingContext context);
}