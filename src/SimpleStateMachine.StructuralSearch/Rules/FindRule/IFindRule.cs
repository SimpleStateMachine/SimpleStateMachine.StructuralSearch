namespace SimpleStateMachine.StructuralSearch.Rules
{
    public interface IFindRule
    {
        bool IsApplicableForPlaceholder(string placeholderName);
        bool Execute(ref IParsingContext context);
    }
}