using SimpleStateMachine.StructuralSearch.Context;

namespace SimpleStateMachine.StructuralSearch.Templates.ReplaceTemplate;

internal interface IReplaceStep
{
    string GetValue(ref IParsingContext context);
}