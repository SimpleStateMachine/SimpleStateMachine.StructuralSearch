using SimpleStateMachine.StructuralSearch.Context;

namespace SimpleStateMachine.StructuralSearch.CustomParsers;

internal interface IContextDependent
{
    void SetContext(ref IParsingContext context);
}