using SimpleStateMachine.StructuralSearch.Context;

namespace SimpleStateMachine.StructuralSearch.Replace;

internal interface IReplaceCondition
{
    bool Execute(ref IParsingContext context);
}