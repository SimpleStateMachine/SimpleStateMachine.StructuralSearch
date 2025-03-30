using SimpleStateMachine.StructuralSearch.Context;

namespace SimpleStateMachine.StructuralSearch;

internal interface IReplaceBuilder
{
    string Build(ref IParsingContext context);
}