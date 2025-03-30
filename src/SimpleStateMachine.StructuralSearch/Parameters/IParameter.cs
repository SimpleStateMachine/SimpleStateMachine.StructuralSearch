using SimpleStateMachine.StructuralSearch.Context;

namespace SimpleStateMachine.StructuralSearch.Parameters;

internal interface IParameter
{
    bool IsApplicableForPlaceholder(string placeholderName);
    string GetValue(ref IParsingContext context);
}