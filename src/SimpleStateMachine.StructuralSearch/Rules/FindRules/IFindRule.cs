using SimpleStateMachine.StructuralSearch.Context;

namespace SimpleStateMachine.StructuralSearch.Rules.FindRules;

public interface IFindRule
{
    bool Execute(ref IParsingContext context);
}