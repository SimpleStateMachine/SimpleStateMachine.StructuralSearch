namespace SimpleStateMachine.StructuralSearch.Rules;

internal interface IFindRule
{
    bool IsApplicableForPlaceholder(string placeholderName);
    bool Execute(ref IParsingContext context);
}