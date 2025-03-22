namespace SimpleStateMachine.StructuralSearch.Rules;

internal class EmptyRule: IFindRule
{
    public bool Execute(ref IParsingContext context) 
        => true;

    public bool IsApplicableForPlaceholder(string placeholderName)
        => false;
}

internal static class Rule
{
    public static readonly EmptyRule Empty = new();
}