using SimpleStateMachine.StructuralSearch.Context;

namespace SimpleStateMachine.StructuralSearch.Templates.ReplaceTemplate;

internal interface IReplaceBuilder
{
    string Build(ref IParsingContext context);
}