namespace SimpleStateMachine.StructuralSearch.Rules;

public class EmptyRule: IRule
{
    public bool Execute(ref IParsingContext context)
    {
        return true;
            
    }
}

public static class Rule
{
    public static readonly EmptyRule Empty = new();
}