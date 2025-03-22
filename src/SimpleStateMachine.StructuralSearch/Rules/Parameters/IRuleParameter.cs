using SimpleStateMachine.StructuralSearch.Context;

namespace SimpleStateMachine.StructuralSearch.Rules.Parameters;

internal interface IRuleParameter
{
    string GetValue(ref IParsingContext context);
}