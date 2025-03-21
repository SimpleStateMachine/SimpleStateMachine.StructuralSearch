namespace SimpleStateMachine.StructuralSearch.Rules;

public class EmptyRule: IFindRule
{
    public bool Execute(ref IParsingContext context) 
        => true;

    public bool IsApplicableForPlaceholder(string placeholderName)
        => false;
}

public static class Rule
{
    public static readonly EmptyRule Empty = new();
}