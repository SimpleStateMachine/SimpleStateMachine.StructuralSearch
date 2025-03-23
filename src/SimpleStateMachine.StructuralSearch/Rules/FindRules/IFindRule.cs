using SimpleStateMachine.StructuralSearch.Context;

namespace SimpleStateMachine.StructuralSearch.Rules.FindRules;

public interface IFindRule
{
    bool IsApplicableForPlaceholder(string placeholderName);
    bool Execute(ref IParsingContext context);
}