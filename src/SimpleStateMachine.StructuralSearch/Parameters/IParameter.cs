using SimpleStateMachine.StructuralSearch.Context;

namespace SimpleStateMachine.StructuralSearch.Parameters;

internal interface IParameter
{
    string GetValue(ref IParsingContext context);
}