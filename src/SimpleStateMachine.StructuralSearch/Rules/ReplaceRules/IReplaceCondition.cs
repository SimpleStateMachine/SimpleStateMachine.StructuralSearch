using SimpleStateMachine.StructuralSearch.Context;

namespace SimpleStateMachine.StructuralSearch.Rules.ReplaceRules;

internal interface IReplaceCondition
{
    bool Execute(ref IParsingContext context);
}